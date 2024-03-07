using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class BetState : State
    {
        public BetState(HandFSM fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
           // throw new System.NotImplementedException();
        }

        public override void Execute()
        {
           // throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            _fsm.HasBet();
        }

        public override State Transition()
        {
            return new WaitState(_fsm);
        }


    }
}