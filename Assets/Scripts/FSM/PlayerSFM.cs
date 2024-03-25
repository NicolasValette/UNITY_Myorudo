using Myorudo.Actions;
using Myorudo.Datas;
using Myorudo.FSM.States;
using Myorudo.FSM.States.PlayerState;
using Myorudo.Game;
using Myorudo.Interfaces.Dice;
using Myorudo.Interfaces.FSM;
using Myorudo.Interfaces.Game;
using Myorudo.Player;
using Palmmedia.ReportGenerator.Core.Parser.Analysis;
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
        [SerializeField]
        protected GameObject _diceRollerGameObject;
        [Header("Human relative")]
        [SerializeField]
        protected PlayerInput _inputProvider;

        protected DudoHandler _dudoHandler;

        protected IRollDice _diceRollerProvider;

        public PlayerType Type { get => _playerType; }
        public bool ReadyToRoll { get; private set; }

        public bool RollFinished { get; private set; }

        public bool ActiveTurn { get; private set; }

        public bool RoundOver { get; private set; }

        public bool HasDudo { get; private set; }

        public bool HasBet { get; private set; }

        public bool ReadyToBet { get; private set; }
        public bool IsEliminated { get; private set; }

        public int PlayerId { get; private set; }
        public bool BetIsDone { get => _betIsDone; }
        protected bool _betIsDone = false;

        protected List<int> _diceResult = new List<int>();
        public List<int> DiceResult { get { return _diceResult; } }
        protected int _numberOfDiceLeft;
        public int NumberOfDiceLeft { get => DiceResult.Count; }

        protected State _currentState;
        public State CurrentState { get { return _currentState; } }

        public event Action OnTurnOver;
        public event Action<List<int>> OnRollFinished;
        public event Action OnRollConfirmed;
        public event Action OnDudo;
        
        


        // Start is called before the first frame update
        void Start()
        {
            _diceRollerProvider = _diceRollerGameObject.GetComponent<IRollDice>();
            if (_diceRollerProvider == null)
            {
                Debug.LogError($"Missing Roller component in {name}");
            }
            _diceRollerProvider.OnRollResult += ReceiveRollResult;
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
            _diceRollerProvider.OnRollResult -= ReceiveRollResult;
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

        //protected void OnElimination(bool isHumain)
        //{
        //    OnPlayerEliminated?.Invoke(isHumain);
        //}
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
            if (!RoundOver)
            {
                OnTurnOver?.Invoke();
            }
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
            _diceRollerProvider.RollDice(_numberOfDiceLeft);

            
        }
        public void ReceiveRollResult(List<int> diceResult)
        {
            _diceResult = diceResult;
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
        public void Eliminate()
        {
            NextTurn.RollDice -= PrepareToRoll;
            IsEliminated = true;
        }
        public void PrepareToStart(int playerID, DudoHandler handler)
        {
            _dudoHandler = handler;
            PlayerId = playerID;
            IsEliminated = false;
            if (_isDebugMode) Debug.Log($"FSM for {_playerType.ToString()} player {PlayerId} is ready");
        }
        public void DecreaseId()
        {
            PlayerId--;
        }
        public abstract void ChooseDudoOrBet();

        public virtual bool LooseDices(int numberOfDices)
        {
            Debug.Log($"Player#{PlayerId} loose {numberOfDices} dice(s)");
            _numberOfDiceLeft -= numberOfDices;
            if (_numberOfDiceLeft == 1)
            {
                _dudoHandler.Palifico();
            }
            return _numberOfDiceLeft <= 0;
        }
        public void Dudo()
        {
            if (_isDebugMode) Debug.Log($"Player {PlayerId} dudo the previous bid {_betProvider.CurrentBid}");
            OnDudo?.Invoke();
            _dudoHandler.RevealHandAndCheckDudoCorrect(_betProvider.CurrentBid, PlayerId);
            //if ()
            //{
            //    // active player win his dudo
            //    OnRoundWin?.Invoke((PlayerId - 1) < 0 ? _gamesRulesData.NumberOfPlayer - 1 : PlayerId - 1);
            
            //}
            //else
            //{
            //    OnRoundWin?.Invoke(PlayerId);
               
            //}

        }
        //public void Bet()
        //{
        //    _betProvider.MakeBet();
        //}
        public void Play()
        {
            if (_currentState is EliminatedState)
            {
                Debug.Log($"Player #{PlayerId} is ELIMINATED");
                
               // OnTurnOver?.Invoke();
            }
            else
            {
                if (_isDebugMode) Debug.Log($"[Player #{PlayerId} ({_playerType}] : Start of turn!");
                ActiveTurn = true;        
            }

        }
        public void FinishRound()
        {
            ActiveTurn = false;
            RoundOver = true;
        }
        #endregion

        
    }
}