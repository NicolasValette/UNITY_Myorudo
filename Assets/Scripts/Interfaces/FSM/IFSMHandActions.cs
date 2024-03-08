using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.FSM
{
    public interface IFSMHandActions
    {
        bool IsLook { get; }
        bool IsRoll { get; }
        bool IsRoundOver { get; } 
        bool IsBet { get; } 
        bool IsDudo { get; }
        bool IsDiceInHand { get; }
        void StopLook();
        void HasBet();
        void StartRound();

        void MoveDice();
        void RollDice();
    }
}