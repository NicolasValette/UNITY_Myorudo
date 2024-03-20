
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
        event Action OnDudo;
        event Action<int> OnRoundWin;
        List<int> DiceResult { get; }
        void PrepareToStart(int playerId, DudoHandler handler);
        void LooseDices(int numberOfDices);
        void FinishRound();
        void Play();
    }
}