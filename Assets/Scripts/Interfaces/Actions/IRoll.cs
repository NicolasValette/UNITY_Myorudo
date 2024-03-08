using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Interfaces.Actions
{


    public interface IRoll
    {
        public void Roll();
        public void TakeDice(Vector3 position);
        public void RoundOver();
        public void Bet();
        public void Look();
        public void Dudo();


    }
}