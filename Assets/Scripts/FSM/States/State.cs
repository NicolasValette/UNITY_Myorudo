using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public abstract class State
    {
        protected HandFSM _fsm;

        public State (HandFSM fsm)
        {
            _fsm = fsm;
        }
        public abstract void EnterState();
        public abstract void Execute();
        public abstract void ExitState();
        public abstract State Transition();

    }
}
