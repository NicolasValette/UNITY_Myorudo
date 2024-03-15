using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.FSM
{
    public interface ISFMActions
    {
        bool ReadyToRoll { get; }
        bool RollFinished { get; }
        bool ActiveTurn { get; }
        bool RoundOver { get; }
        bool HasDudo { get; }
        bool HasBet { get; }
        bool ReadyToBet { get; }

        void RollDice();
    }
}