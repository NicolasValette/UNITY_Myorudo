using Myorudo.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Actions
{
    public class PlayerBet : Bet
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public override void MakeBet ()
        {
            OnBetChanged(new Bid(1,1));
        }

        private void PrepareBet(Bid previousBid)
        {
            OnPrepareBet(previousBid);
        }
        #region EVENTS
        protected override void OnBetChanged(Bid bid)
        {

            // Call the base class event invocation method.
            base.OnBetChanged(bid);
        }
        protected override void OnPrepareBet(Bid bid)
        {

            // Call the base class event invocation method.
            base.OnPrepareBet(bid);
        }
        #endregion
    }
}