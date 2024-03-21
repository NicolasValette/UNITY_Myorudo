using Myorudo.Datas;
using Myorudo.Game;
using Myorudo.Interfaces.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Myorudo.Game
{
    [RequireComponent(typeof(NextTurn))]
    public class DudoHandler : MonoBehaviour
    {
        [SerializeField]
        private GameRulesData _gameRulesData;
        [SerializeField]
        private GameObject _poolPanel;
        [SerializeField]
        private List<TMP_Text> _poolText;

        [SerializeField]
        private GameObject _dudoPanel;
        [SerializeField]
        private TMP_Text _dudoText;

        private int[] _dicePool;
        private List<IPlay> _playerList;

        public static event Action OnDudoRevealHand;

        private void OnEnable()
        {
            NextTurn.RollDice += InitDicesPool;
        }
        private void OnDisable()
        {
            NextTurn.RollDice -= InitDicesPool;
            var playerList = GetComponent<NextTurn>().PlayerList;
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].OnRollFinished -= RollDone;
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            _poolPanel.SetActive(false);
            _dudoPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void RollDone(List<int> diceResult)
        {
            for (int i = 0; i < diceResult.Count; i++)
            {
                _dicePool[diceResult[i]-1]++;
            }
        }

      
        public void InitDicesPool()
        {
            _dicePool = new int[_gameRulesData.NumberOfFace];
            var playerList = GetComponent<NextTurn>().PlayerList;
            for (int i=0; i < playerList.Count; i++)
            {
                playerList[i].OnRollFinished += RollDone;
            }
        }

        public void InitBeforeStart(int diceFaces, List<IPlay> playerList)
        {
            _dicePool = new int[diceFaces + 1];
            _playerList = new List<IPlay>();
            _playerList = playerList;
        }

        private void RevealPool()
        {
            _dudoPanel.SetActive(true);
            _poolPanel.SetActive(true);

            _dudoText.text = $"Player#{NextTurn.CurrentPlayer} yell DUDO !!!";
            for (int i=0;i<_poolText.Count;i++)
            {
                _poolText[i].text = $"{i+1} ; {_dicePool[i]}";
            }
        }
        public bool RevealHandAndCheckDudoCorrect(Bid bid)
        {
            RevealPool();
            OnDudoRevealHand?.Invoke();
            return _dicePool[bid.Face] <= bid.Value;
        }
    }
}