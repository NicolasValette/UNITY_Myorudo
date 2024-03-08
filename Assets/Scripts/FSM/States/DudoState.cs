using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class DudoState : State
    {
        public DudoState(IFSMHandActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            //throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            //throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            //throw new System.NotImplementedException();
        }

        public override State Transition()
        {
            return new IdleState(_fsm);
        }

      
    }
}