using Myorudo.Datas;
using Myorudo.FSM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        public override Bid MakeFirstBet(List<int> diceResult)
        {
            _currentBid = diceResult.GroupBy(x => x).Select(x => new Bid (x.Count(), x.Key)).OrderByDescending(x => x.Value).First();
            OnBetChanged(_currentBid);
            //diceResult.Select(x=>x);
            return _currentBid;
        }
        public override Bid MakeBet(Bid bid = null)
        {
           
            Bid newbid = new Bid(CurrentBid.Value + 1, CurrentBid.Face);
            OnBetChanged(newbid);
            _currentBid = newbid;
            return _currentBid;
        }
        protected override void OnBetChanged(Bid bid)
        {

            // Call the base class event invocation method.
            base.OnBetChanged(bid);
        }
     
    }
}