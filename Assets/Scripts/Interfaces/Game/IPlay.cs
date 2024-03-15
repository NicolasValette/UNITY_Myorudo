using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Game
{
    public interface IPlay
    {
        event Action OnTurnOver;
        event Action OnRollFinished;
        void PrepareToStart(int playerId);
        void Play();
    }
}