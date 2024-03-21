using Myorudo.Actions;
using Myorudo.Datas;
using Myorudo.FSM.States;
using Myorudo.FSM.States.PlayerState;
using Myorudo.Game;
using Myorudo.Interfaces.Dice;
using Myorudo.Interfaces.FSM;
using Myorudo.Interfaces.Game;
using Myorudo.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

namespace Myorudo.FSM
{
    public enum PlayerType
    {
        Human,
        IA
    }
    public abstract class PlayerSFM : MonoBehaviour, ISFMActions, IPlay
    {
        [SerializeField]
        protected bool _isDebugMode = false;
        [SerializeField]
        protected PlayerType _playerType;
        [SerializeField]
        protected GameRulesData _gamesRulesData;
        [SerializeField]
        protected Bet _betProvider;
        [SerializeField]
        protected Dudo _dudoProvider;
        [Header("Human relative")]
        [SerializeField]
        protected PlayerInput _inputProvider;

        protected DudoHandler _dudoHandler;

        protected IRollDice _diceRollerProvider;

        public bool ReadyToRoll { get; private set; }

        public bool RollFinished { get; private set; }

        public bool ActiveTurn { get; private set; }

        public bool RoundOver { get; private set; }

        public bool HasDudo { get; private set; }

        public bool HasBet { get; private set; }

        public bool ReadyToBet { get; private set; }

        public int PlayerId { get; private set; }
        public bool BetIsDone { get => _betIsDone; }
        protected bool _betIsDone = false;

        protected List<int> _diceResult = new List<int>();
        public List<int> DiceResult { get { return _diceResult; } }
        protected int _numberOfDiceLeft;

        protected State _currentState;
        public State CurrentState { get { return _currentState; } }

        public event Action OnTurnOver;
        public event Action<List<int>> OnRollFinished;
        public event Action OnRollConfirmed;
        public event Action OnDudo;
        public event Action<int> OnRoundWin;


        // Start is called before the first frame update
        void Start()
        {
            _diceRollerProvider = GetComponent<IRollDice>();
            _numberOfDiceLeft = _gamesRulesData.NumberOfStartingDices;
            InitSFM();
        }
        private void OnEnable()
        {
            if (_playerType == PlayerType.IA)
            {
                NextTurn.RollDice += PrepareToRoll;
            }
        }
        private void OnDisable()
        {
            NextTurn.RollDice -= PrepareToRoll;
        }

        // Update is called once per frame
        void Update()
        {
            _currentState.Execute();
            State _nextState = _currentState.GetNextState();
            if (_nextState != null)
            {
                Transition(_nextState);
            }
        }

        protected void InitSFM()
        {
            _currentState = new WaitingRollState(this);
            PrepateForNextRound();
        }
        public void StartRound()
        {
            _currentState = new WaitingRollState(this);
        }


        public void PrepareToRoll()
        {
            ReadyToRoll = true;
        }

        /// <summary>
        /// Switch the machine state from one state to the next state
        /// </summary>
        /// <param name="nextState"></param>
        protected void Transition(State nextState)
        {
            string prevState = _currentState.ToString();

            _currentState.ExitState();
            _currentState = nextState;
            _currentState.EnterState();

            string debugStr = $"### Change state from ({prevState}) to ({_currentState}) for FSM {PlayerId} ###";
            if (_isDebugMode) Debug.Log(debugStr);
        }

        public void EndTurn()
        {
            // We call the event
            if (_isDebugMode) Debug.Log($"[Player #{PlayerId} ({_playerType}] : End of turn!");
            ActiveTurn = false;
            _betIsDone = false;
            OnTurnOver?.Invoke();
        }
        public void EndRoll()
        {
           
            RollFinished = true;
            OnRollConfirmed?.Invoke();
        }

        #region intercafe ISFMActions
        public void PlayTurn()
        {
            Play();
        }
        public virtual void RollDice()
        {
            ReadyToRoll = false;
            _diceResult = _diceRollerProvider.RollDice(_numberOfDiceLeft);
            if (_isDebugMode)
            {
                StringBuilder strb = new StringBuilder();
                for (int i = 0; i < _diceResult.Count; i++)
                {
                    strb.Append($"-{_diceResult[i]}");
                }
                strb.Append("-");
                Debug.Log($"Dice result for Player #{PlayerId} [{strb}]");
            }
            OnRollFinished?.Invoke(_diceResult);
        }

        public void PrepateForNextRound()
        {
            ReadyToRoll = false;
            RollFinished = false;
            ActiveTurn = false;
            RoundOver = false;
            HasDudo = false;
            HasBet = false;
            ReadyToBet = false;
            _betIsDone = false;
        }

        #endregion

        #region INTERFACE IPLAY
        public void PrepareToStart(int playerID, DudoHandler handler)
        {
            _dudoHandler = handler;
            PlayerId = playerID;
            if (_isDebugMode) Debug.Log($"FSM for {_playerType.ToString()} player {PlayerId} is ready");
        }
        public abstract void ChooseDudoOrBet();

        public void LooseDices(int numberOfDices)
        {
            Debug.Log($"Player#{PlayerId} loose {numberOfDices}");
            _numberOfDiceLeft -= numberOfDices;
        }
        public void Dudo()
        {
            OnDudo?.Invoke();

            if (_dudoHandler.RevealHandAndCheckDudoCorrect(_betProvider.CurrentBid))
            {
                // active player win his dudo
                OnRoundWin?.Invoke((PlayerId - 1) < 0 ? _gamesRulesData.NumberOfPlayer - 1 : PlayerId - 1);

            }
            else
            {
                OnRoundWin?.Invoke(PlayerId);
            }

        }
        //public void Bet()
        //{
        //    _betProvider.MakeBet();
        //}
        public void Play()
        {
            if (_isDebugMode) Debug.Log($"[Player #{PlayerId} ({_playerType}] : Start of turn!");
            ActiveTurn = true;

        }
        public void FinishRound()
        {
            ActiveTurn = false;
            RoundOver = true;
        }
        #endregion

        
    }
}