using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Dice
{
    public interface IRollDice
    {
        public List<int> RollDice(int nbOfDices);
    }
}