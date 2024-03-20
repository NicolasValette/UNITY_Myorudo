using Myorudo.Datas;
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
                if (_isDebugMode) Debug.Log($"Player {PlayerId} dudo the previous bid {_betProvider.CurrentBid}");
            }
            else
            {
                //bet
                Bid bid = _betProvider.MakeBet();
                if (_isDebugMode) Debug.Log($"Player {PlayerId} bet {bid}");
            }

        }
    }
}