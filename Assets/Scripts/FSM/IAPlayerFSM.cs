using Myorudo.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM
{
    public class IAPlayerFSM : PlayerSFM
    {
        [SerializeField]
        private IAPlayerData _iaPlayerData;
        public override void ChooseDudoOrBet()
        {
            if (_dudoProvider.YellDudo(_betProvider.CurrentBid))
            {
                //dudo
                Dudo();
               
            }
            else
            {
                //bet
                StartCoroutine(Wait(CallBet));
            }
        }
        public void CallBet()
        {
            Bid bid;
            if (_betProvider.CurrentBid == null)
            {
                //It's the first turn
                bid = _betProvider.MakeFirstBet(_diceResult);
            }
            else
            {
                bid = _betProvider.MakeBet();
            }
            _betIsDone = true;
            if (_isDebugMode) Debug.Log($"Player #{PlayerId} (IA) bet {bid}");
        }
        protected IEnumerator Wait(Action action)
        {
            yield return new WaitForSeconds(_iaPlayerData.WaitingTime);
            action.Invoke();
        }
       
        public override void RollDice()
        {
            base.RollDice();
            EndRoll();
        }
        public override bool LooseDices(int numberOfDices)
        {
            var result = base.LooseDices(numberOfDices);
            if (_numberOfDiceLeft <= 0)
            {
                Debug.Log($"Player #{PlayerId} is eliminated !");
                //base.OnElimination(false);
            }
            return result;
        }
    }
}