using Myorudo.Datas;
using Myorudo.Game;
using Myorudo.Interfaces.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Game
{
    [RequireComponent(typeof(NextTurn))]
    public class DudoHandler : MonoBehaviour
    {
        private int[] _dicePool;
        private List<IPlay> _playerList;

        public static event Action OnDudoRevealHand;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RollDone(List<int> diceResult)
        {
            for (int i = 0; i < diceResult.Count; i++)
            {
                _dicePool[diceResult[i]]++;
            }
        }

        public void InitBeforeStart(int diceFaces, List<IPlay> playerList)
        {
            _dicePool = new int[diceFaces + 1];
            _playerList = new List<IPlay>();
            _playerList = playerList;
        }
        public bool RevealHandAndCheckDudoCorrect(Bid bid)
        {
            OnDudoRevealHand?.Invoke();
            return _dicePool[bid.Face] <= bid.Value;
        }
    }
}