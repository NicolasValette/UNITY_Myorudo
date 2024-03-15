using Myorudo.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Actions
{
    public abstract class Dudo : MonoBehaviour
    {
        [SerializeField]
        protected GameRulesData _gameRulesData;
        public abstract bool YellDudo(Bid previousBid);
    }
}