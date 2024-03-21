using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States.PlayerState
{
    public class RollDiceState : State
    {
        public RollDiceState(ISFMActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            _fsm.RollDice();
        }

        public override void Execute()
        {
            
        }

        public override void ExitState()
        {
            
        }

        public override State GetNextState()
        {
            return _fsm.RollFinished ? new WaitTurnState(_fsm) : null;
        }
    }
}