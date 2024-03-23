
using Myorudo.FSM;
using Myorudo.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Game
{
    public interface IPlay
    {
        event Action OnTurnOver;
        event Action<List<int>> OnRollFinished;
        event Action OnRollConfirmed;
        event Action OnDudo;

        PlayerType Type { get; }
        List<int> DiceResult { get; }
        bool IsEliminated { get; }
        void PrepareToStart(int playerId, DudoHandler handler);
        bool LooseDices(int numberOfDices);
        void FinishRound();
        void Play();
        void DecreaseId();
        void Eliminate();
    }
}