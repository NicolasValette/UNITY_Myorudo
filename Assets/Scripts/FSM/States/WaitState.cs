using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class WaitState : State
    {
        public WaitState (IFSMHandActions fsm) : base (fsm) 
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
            if (_fsm.IsLook)
            {
                return new LookState(_fsm);
            }
            else if (_fsm.IsBet)
            {
                return new BetState(_fsm);
            }
            else if (_fsm.IsDudo)
            {
                return new DudoState(_fsm);
            }
            else
            {
                return null;
            }
        }
    }
}