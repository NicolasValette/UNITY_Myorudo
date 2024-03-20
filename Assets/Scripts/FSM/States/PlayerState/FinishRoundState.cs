using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States.PlayerState
{
    public class FinishRoundState : State
    {
        public FinishRoundState(ISFMActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            //Do nothing
        }

        public override void Execute()
        {
           //Do Nothing
        }

        public override void ExitState()
        {
           _fsm.PrepateForNextRound();
        }

        public override State GetNextState()
        {
            return new WaitingRollState(_fsm);
        }
    }
}