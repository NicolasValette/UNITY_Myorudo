using Myorudo.Datas;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Actions
{
    public class IABet : Bet
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void MakeBet()
        {
            Bid newbid = new Bid(CurrentBid.Value + 1, CurrentBid.Face);
            OnBetChanged(newbid);
        }
        protected override void OnBetChanged(Bid bid)
        {

            // Call the base class event invocation method.
            base.OnBetChanged(bid);
        }
    }
}