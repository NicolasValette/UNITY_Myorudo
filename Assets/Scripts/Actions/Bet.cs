using Myorudo.Datas;
using Myorudo.FSM;
using Myorudo.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.CullingGroup;

namespace Myorudo.Actions
{

    public abstract class Bet : MonoBehaviour
    {
        public static event Action<int, Bid> OnBet;
        public static event Action<Bid> PrepareBet;

        protected static Bid _currentBid;
        public Bid CurrentBid { get => _currentBid; }

        [SerializeField]
        protected bool _isDebugMode;

        private void OnEnable()
        {
            NextTurn.PrepareNextRound += NextRound;
        }
        private void OnDisable()
        {
            NextTurn.PrepareNextRound -= NextRound;
        }
        protected virtual void OnBetChanged(Bid bid)
        {
            _currentBid = bid;
            // Safely raise the event for all subscribers
            OnBet?.Invoke(GetComponent<PlayerSFM>().PlayerId, bid);
        }
        protected virtual void OnPrepareBet(Bid bid)
        {
            // Safely raise the event for all subscribers
            PrepareBet?.Invoke(bid);
        }

        public virtual Bid MakeFirstBet(List<int> diceResult)
        {
            return null;
        }
        public void NextRound()
        {
            _currentBid = null;
        }
        public abstract Bid MakeBet(Bid bid = null);
    }
}