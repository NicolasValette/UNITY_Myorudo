using Myorudo.FSM.States;
using Myorudo.Interfaces.Actions;
using UnityEngine;

namespace Myorudo.FSM
{
    public class HandFSM : MonoBehaviour, IRoll
    {
        #region Serialized Fields
        [SerializeField]
        private bool _isDebugMode = false;
        #endregion

        private State _currentState;
        public State CurrentState { get { return _currentState; } }
        public bool IsDiceInHand { get; private set; } = false;
        public bool IsLook { get; private set; } = false;
        public bool IsRoll { get; private set; } = false;
        public bool IsRoundOver { get; private set; } = false;
        public bool IsBet { get; private set; } = false;
        public bool IsDudo { get; private set; } = false;
        // Start is called before the first frame update
        void Start()
        {
            InitFMS();
        }

        // Update is called once per frame
        void Update()
        {
            _currentState.Execute();
            State _nextState = _currentState.Transition();
            if (_nextState != null)
            {
                ChangeState(_nextState);
            }
        }
        private void InitFMS()
        {
           _currentState = new IdleState(this);
        }
        private void ChangeState(State nextState)
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
        public void TakeDice()
        {
            IsDiceInHand = true;
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
    }
}