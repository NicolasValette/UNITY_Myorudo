using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class LookState : State
    {
        public LookState(IFSMHandActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            //throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            Debug.Log("Look");
            //throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            _fsm.StopLook();
            //throw new System.NotImplementedException();
        }

        public override State Transition()
        {
            return new WaitState(_fsm);
            //throw new System.NotImplementedException();
        }
    
    }
}