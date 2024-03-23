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
            _fsm.ChooseDudoOrBet();
        }

        public override void Execute()
        {
            
        }

        public override void ExitState()
        {
            _fsm.EndTurn();
        }

        public override State GetNextState()
        {

            if (_fsm.IsEliminated)
            {
                return new EliminatedState(_fsm);
            }
            else if (_fsm.RoundOver)
            {
                return new FinishRoundState(_fsm);
            }
            else if (_fsm.BetIsDone)
            {
                return new WaitTurnState(_fsm);
            }
            else
            {
                return null;
            }
        }

    }
}