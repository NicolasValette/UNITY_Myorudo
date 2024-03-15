using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States.PlayerState
{
    public class ActiveTurnState : State
    {

        public ActiveTurnState(ISFMActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
          //  _fsm.PrepareDudo();
        }

        public override void Execute()
        {
            
        }

        public override void ExitState()
        {
           
        }

        public override State GetNextState()
        {
            if (_fsm.HasDudo)
            {
                return new TurnDudoState(_fsm);
            }
            else if (_fsm.ReadyToBet)
            {
                return new BetTurnState(_fsm);
            }
            else
            {
                return null;
            }
        }

    }
}