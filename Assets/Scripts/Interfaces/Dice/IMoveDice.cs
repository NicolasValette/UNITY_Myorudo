using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Dice
{
    public interface IMoveDice
    {

        List<GameObject> TakeDice(Vector3 position, int numberOfDices);
        void MoveHeldDice (Vector3 position);
        void Roll(Vector2 deltaCursorDirection);
    }
}