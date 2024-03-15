using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States.PlayerState
{
    public class BetTurnState : State
    {
        public BetTurnState(ISFMActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            throw new System.NotImplementedException();
        }

        public override void ExitState()
        {
            throw new System.NotImplementedException();
        }

        public override State GetNextState()
        {
            return _fsm.HasBet ? new WaitTurnState(_fsm) : null;
        }

  
      
    }
}