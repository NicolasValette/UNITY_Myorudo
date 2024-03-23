using Myorudo.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM
{


    public class HumanPlayerFSM : PlayerSFM
    {
        public event Action OnActiveTurn;
        public override void ChooseDudoOrBet()
        {
            OnActiveTurn?.Invoke();
            //TODO
        }
        public override void RollDice()
        {
            base.RollDice();
        }
        public Bid PreviousBid()
        {
            return _betProvider.CurrentBid;
        }
        public void Bet(Bid bid)
        {
            _betProvider.MakeBet(bid);
            if (_isDebugMode) Debug.Log($"Player #{PlayerId}(Human) bet {bid}");
            _betIsDone = true;                
        }
        public override bool LooseDices(int numberOfDices)
        {
            var result = base.LooseDices(numberOfDices);
            if (_numberOfDiceLeft <= 0)
            {
                Debug.Log($"Human player #{PlayerId} is eliminated !");
                //OnElimination(true);
            }
            return result;
        }
    }
}