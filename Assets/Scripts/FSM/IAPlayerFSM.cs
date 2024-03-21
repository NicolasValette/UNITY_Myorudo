using Myorudo.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.FSM
{
    public class IAPlayerFSM : PlayerSFM
    {
        public override void ChooseDudoOrBet()
        {
            if (_dudoProvider.YellDudo(_betProvider.CurrentBid))
            {
                //dudo
                Dudo();
                if (_isDebugMode) Debug.Log($"Player {PlayerId} dudo the previous bid {_betProvider.CurrentBid}");
            }
            else
            {
                //bet
                StartCoroutine(Wait(1f, CallBet));
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
        protected IEnumerator Wait(float waitTime, Action action)
        {
            yield return new WaitForSeconds(waitTime);
            action.Invoke();
        }
       
        public override void RollDice()
        {
            base.RollDice();
            EndRoll();
        }
    }
}