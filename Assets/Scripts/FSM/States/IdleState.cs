using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class IdleState : State
    {
        public IdleState(IFSMHandActions fsm) : base(fsm)
        {
        }
        public override void EnterState()
        {
            _fsm.StartRound();
        }

        public override void Execute()
        {
            //Do Nothing
        }

        public override void ExitState()
        {
           // throw new System.NotImplementedException();
        }

        public override State Transition()
        {
            return (_fsm.IsDiceInHand) ? new HoldDiceState(_fsm):null ;
        }
    }
}