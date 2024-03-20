using Myorudo.Datas;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Myorudo.Actions
{

    public abstract class Bet : MonoBehaviour
    {
        public static event Action<Bid> OnBet;
        public static event Action<Bid> PrepareBet;

        private static Bid _currentBid;
        public Bid CurrentBid { get => _currentBid; }

        protected virtual void OnBetChanged(Bid bid)
        {
            _currentBid = bid;
            // Safely raise the event for all subscribers
            OnBet?.Invoke(bid);
        }
        protected virtual void OnPrepareBet(Bid bid)
        {
            // Safely raise the event for all subscribers
            PrepareBet?.Invoke(bid);
        }

        public abstract Bid MakeBet();
    }
}