using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States.PlayerState
{
    public class WaitingRollState : State
    {
        public WaitingRollState(ISFMActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
           
        }

        public override void Execute()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override State GetNextState()
        {
            return (_fsm.ReadyToRoll) ? new RollDiceState(_fsm) : null;
        }
    }
}