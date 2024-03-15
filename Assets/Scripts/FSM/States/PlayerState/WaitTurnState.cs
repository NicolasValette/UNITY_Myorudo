using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States.PlayerState
{
    public class WaitTurnState : State
    {
        public WaitTurnState(ISFMActions fsm) : base(fsm)
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
            if (_fsm.RoundOver)
            {
                return new WaitingRollState(_fsm);
            }
            else if (_fsm.ActiveTurn)
            {
                return new ActiveTurnState(_fsm);
            }
            else
            {
                return null;
            }
        }
    }
}