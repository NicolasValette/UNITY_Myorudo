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
    public class PlayerSFM : MonoBehaviour, ISFMActions, IPlay
    {
        [SerializeField]
        private bool _isDebugMode = false;
        [SerializeField]
        private PlayerType _playerType;
        [SerializeField]
        private GameRulesData _gamesRulesData;
        [SerializeField]
        private Bet _betProvider;
        [SerializeField]
        private Dudo _dudoProvider;
        [Header("Human relative")]
        [SerializeField]
        private PlayerInput _inputProvider;

        private IRollDice _diceRollerProvider;

        public bool ReadyToRoll { get; private set; }

        public bool RollFinished { get; private set; }

        public bool ActiveTurn { get; private set; }

        public bool RoundOver { get; private set; }

        public bool HasDudo { get; private set; }

        public bool HasBet { get; private set; }

        public bool ReadyToBet { get; private set; }

        public int PlayerId { get; private set; }

        private List<int> _diceResult = new List<int>();
        private int _numberOfDiceLeft;
        
        private State _currentState;

        public event Action OnTurnOver;
        public event Action OnRollFinished;


        // Start is called before the first frame update
        void Start()
        {
            _diceRollerProvider = GetComponent<IRollDice>();
            _numberOfDiceLeft = _gamesRulesData.NumberOfStartingDices;
            InitSFM();
        }
        private void OnEnable()
        {
            NextTurn.RollDice += PrepareToRoll;
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

        private void InitSFM()
        {
            _currentState = new WaitingRollState(this);
           
        }
        public void StartRound()
        {
            _currentState = new WaitingRollState(this);            
        }

        public void looseDices (int numberOfDices)
        {
            _numberOfDiceLeft -= numberOfDices;
        }
        public void PrepareToRoll()
        {
            ReadyToRoll = true;
        }

        /// <summary>
        /// Switch the machine state from one state to the next state
        /// </summary>
        /// <param name="nextState"></param>
        private void Transition(State nextState)
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
            OnTurnOver?.Invoke();
        }

        #region intercafe ISFMActions
        public void RollDice()
        {
            ReadyToRoll = false;
            _diceResult = _diceRollerProvider.RollDice(_numberOfDiceLeft);
            if (_isDebugMode)
            {
                StringBuilder strb = new StringBuilder();
                for (int i =0; i< _diceResult.Count; i++)
                {
                    strb.Append($"-{_diceResult[i]}");
                }
                strb.Append("-");
                Debug.Log($"Dice result for Player #{PlayerId} [{strb}]");
            }
            RollFinished = true;
            OnRollFinished.Invoke();
        }
        
        #endregion

        #region INTERFACE IPLAY
        public void PrepareToStart(int playerID)
        {
            PlayerId = playerID;
            if (_isDebugMode) Debug.Log($"FSM for {_playerType.ToString()} player {PlayerId} is ready");
        }
        public void ChooseDudoOrBet()
        {
            
        }
        public void Dudo()
        {
            _dudoProvider.YellDudo(_betProvider.CurrentBid);
        }
        public void Bet()
        {
            _betProvider.MakeBet();
        }
        public void Play()
        {
            if (_isDebugMode) Debug.Log($"[Player #{PlayerId} ({_playerType}] : Start of turn!");
            ActiveTurn = true;
            
        }
        
        #endregion
    }
}