using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Dice
{
    public interface IRollDice
    {
        event Action<List<int>> OnRollResult;
        void RollDice(int nbOfDices);
    }
}