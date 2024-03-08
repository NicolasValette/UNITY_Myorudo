using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Dice
{
    public interface IMoveDice
    {
        void TakeDice(Vector3 position);
        void MoveHeldDice (Vector3 position);
        void Roll(Vector2 deltaCursorDirection);
    }
}