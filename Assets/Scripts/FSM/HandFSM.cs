using Myorudo.FSM.States;
using Myorudo.Interfaces.Actions;
using Myorudo.Interfaces.Dice;
using Myorudo.Interfaces.FSM;
using Myorudo.Player;
using UnityEngine;

namespace Myorudo.FSM
{
    public class HandFSM : MonoBehaviour, IRoll, IFSMHandActions
    {
        #region Serialized Fields
        [SerializeField]
        private bool _isDebugMode = false;
        [SerializeField]
        private PlayerInput _playerInput;

        #endregion

        private State _currentState;
        public State CurrentState { get { return _currentState; } }
        public bool IsDiceInHand { get; private set; } = false;
        public bool IsLook { get; private set; } = false;
        public bool IsRoll { get; private set; } = false;
        public bool IsRoundOver { get; private set; } = false;
        public bool IsBet { get; private set; } = false;
        public bool IsDudo { get; private set; } = false;

        private IMoveDice _moveDiceProvider;

        // Start is called before the first frame update
        void Start()
        {
            InitFMS();

            _moveDiceProvider = GetComponent<IMoveDice>();
            if (_moveDiceProvider == null)
            {
                Debug.LogError($"Missing componant Move dice provider in {gameObject.name}");
            }

        }

        // Update is called once per frame
        void Update()
        {
            _currentState.Execute();
            State _nextState = _currentState.GetNextState();
            //State _nextState = _currentState.Transition();
            if (_nextState != null)
            {
                Transition(_nextState);
            }
        }
        private void InitFMS()
        {
           //_currentState = new IdleState(this);
        }
        private void Transition(State nextState)
        {
            string prevState = _currentState.ToString();
            _currentState.ExitState();
            _currentState = nextState;
            _currentState.EnterState();
            string debugStr = $"### Change state from ({prevState}) to ({_currentState}) ###";
            if (_isDebugMode) Debug.Log(debugStr);
        }

        public void StartRound()
        {
            IsDiceInHand = false;
            IsRoll = false;
            IsLook = false;
            IsBet = false;
            IsRoundOver = false;
            IsDudo = false;
        }
        public void TakeDice(Vector3 position)
        {
            IsDiceInHand = true;
           // _moveDiceProvider.TakeDice(position);
        }

        public void Roll()
        {
            IsRoll = true;
        }
        public void Look()
        {
            IsLook = true;
        }
        public void StopLook()
        {
            IsLook = false;
        }
        public void Bet()
        {
            IsBet = true;
        }
        public void HasBet()
        {
            IsBet = false;
        }
        public void RoundOver()
        {
            IsRoundOver = true;
        }
        public void Dudo()
        {
            IsDudo = true;
        }

        public void MoveDice()
        {
            _moveDiceProvider.MoveHeldDice(_playerInput.GetPosition());
        }
        public void RollDice()
        {
            Debug.Log("Roll--");
            _moveDiceProvider.Roll(_playerInput.GetCursorDeltaPos());
        }
    }
}