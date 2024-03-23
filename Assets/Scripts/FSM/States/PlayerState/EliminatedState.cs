using Myorudo.FSM.States.PlayerState;
using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class EliminatedState : State
    {
        public EliminatedState(ISFMActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            // Do Nothing
        }

        public override void Execute()
        {
            //Do Nothing
        }

        public override void ExitState()
        {
        
        }

        public override State GetNextState()
        {
            return null;
        }
    }
}