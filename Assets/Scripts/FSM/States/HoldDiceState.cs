using Myorudo.Interfaces.FSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM.States
{
    public class HoldDiceState : State
    {
        public HoldDiceState(IFSMHandActions fsm) : base(fsm)
        {
        }

        public override void EnterState()
        {
            //throw new System.NotImplementedException();
        }

        public override void Execute()
        {
            _fsm.MoveDice();
        }

        public override void ExitState()
        {
            _fsm.RollDice();
        }

        public override State Transition()
        {
            return (_fsm.IsRoll)?new WaitState(_fsm):null;
        }
    }
}