using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class HoldDiceState : State
    {
        public HoldDiceState(HandFSM fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            //throw new System.NotImplementedException();
        }

        public override void Execute()
        {
           // throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
           // throw new System.NotImplementedException();
        }

        public override State Transition()
        {
            return (_fsm.IsRoll)?new WaitState(_fsm):null;
        }
    }
}