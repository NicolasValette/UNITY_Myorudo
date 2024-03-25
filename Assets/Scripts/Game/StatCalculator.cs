using Myorudo.Datas;
using Myorudo.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Game
{
    public class StatCalculator : MonoBehaviour
    {
        [SerializeField]
        private GameRulesData _gameRulesData;
        [SerializeField]
        private NextTurn _turnManager;
        [SerializeField]
        private DudoHandler _dudoHandler;

        private int _diceNumber;
        private float _maxPossibleResult;
        private void OnEnable()
        {
            NextTurn.OnFirstRoundStart += ComputeStat;

        }
        private void OnDisable()
        {
            NextTurn.OnFirstRoundStart -= ComputeStat;
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ComputeStat()
        {
            _diceNumber = 0;
            for (int i = 0; i < _turnManager.PlayerList.Count; i++)
            {
                _diceNumber += _turnManager.PlayerList[i].DiceResult.Count;
            }
            _maxPossibleResult = Mathf.Pow(_gameRulesData.NumberOfFace, _diceNumber);
        }
        public float GetStatFromBid(Bid bid)
        {
            float stat;
            //n = _diceNumber;
            //k = bid.Value
            //Binomial Law ; nCk * p^k * (1-p)^(n-k)
            stat = ((Fact(_diceNumber)) / (Fact(bid.Value) * Fact(_diceNumber - bid.Value)))
                    * Mathf.Pow(1f / _gameRulesData.NumberOfFace, bid.Value)
                    * Mathf.Pow(1f - (1f / _gameRulesData.NumberOfFace), (_diceNumber - bid.Value));

            return (bid.Face == 0 || _dudoHandler.IsPalifico)?stat:stat*2;
        }
        private float Fact(int n)
        {
            float result = 1f;
            for (int i = n; i>0;i--)
            {
                result *= i;
            }
            return result;
        }
    }
}