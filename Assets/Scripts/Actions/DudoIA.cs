using Myorudo.Datas;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Actions
{
    public class DudoIA : Dudo
    {

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public override bool YellDudo(Bid previousBid)
        {

            if (previousBid == null || previousBid.Value <= (_gameRulesData.NumberOfStartingDices * _gameRulesData.NumberOfPlayer) - NextTurn.RoundNumber)
            {
                return false;
            }
            else
            {
                return true;
            }

        }
    }
}