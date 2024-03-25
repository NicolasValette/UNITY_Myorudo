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
        [SerializeField]
        private TMP_Text _winnerDudoText;
        [SerializeField]
        private TMP_Text _palificoText;


        private int[] _dicePool;
        private List<IPlay> _playerList;
        private int _winnerID;
        private int _looserID;

        private bool _isPalifico = false;
        public bool IsPalifico { get => _isPalifico; }

        public static event Action OnDudoRevealHand;
        public static event Action<int> OnRoundWin;

        private void OnEnable()
        {
            NextTurn.OnGameInit += InitPlayer;
            NextTurn.RollDice += InitDicesPool;
            
        }
        private void OnDisable()
        {
            NextTurn.OnGameInit -= InitPlayer;
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
            _palificoText.enabled = false;
            _poolPanel.SetActive(false);
            _dudoPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
          //  _poolPanel.SetActive(true);
            if (_dicePool != null)
            {

                for (int i = 0; i < _poolText.Count; i++)
                {
                    _poolText[i].text = $"{i + 1} ; {_dicePool[i]}";
                }
            }
        }
        public void TogglePool()
        {
            _poolPanel.SetActive(!_poolPanel.activeSelf);
        }
        public void RollDone(List<int> diceResult)
        {
            for (int i = 0; i < diceResult.Count; i++)
            {
                _dicePool[diceResult[i] - 1]++;
            }
        }
        public void InitPlayer()
        {
            var playerList = GetComponent<NextTurn>().PlayerList;
            for (int i = 0; i < playerList.Count; i++)
            {
                playerList[i].OnRollFinished += RollDone;
            }
        }

        public void InitDicesPool()
        {
            if (_dicePool == null)
            {
                _dicePool = new int[_gameRulesData.NumberOfFace];
            }
            else
            {
                for (int i = 0; i< _dicePool.Length; i++)
                {
                    _dicePool[i] = 0;
                }
            }
        }
        public void Palifico()
        {
            _palificoText.enabled = true;
            _isPalifico = true;
        }
        public void InitBeforeStart(int diceFaces, List<IPlay> playerList)
        {
            _dicePool = new int[diceFaces];
            _playerList = new List<IPlay>();
            _playerList = playerList;
        }

        private void RevealPool(int playerID)
        {
            _dudoPanel.SetActive(true);
            _poolPanel.SetActive(true);

            _dudoText.text = $"Player#{playerID} yells DUDO !!!";
            for (int i = 0; i < _poolText.Count; i++)
            {
                _poolText[i].text = $"{i + 1} ; {_dicePool[i]}";
            }
        }
        public void RevealHandAndCheckDudoCorrect(Bid bid, int playerDudoID)
        {
            RevealPool(playerDudoID);
            OnDudoRevealHand?.Invoke();
            if (_isPalifico)
            {
                if (_dicePool[bid.Face - 1] <= bid.Value) // -1 because the tab is between [0 - and (5) ]
                {
                    Debug.Log("win");
                    _winnerDudoText.text = "He's damn right !!";
                    _winnerID = playerDudoID;
                    _looserID = GetComponent<NextTurn>().PreviousPlayer(playerDudoID);
                }
                else
                {
                    Debug.Log("loose");
                    _winnerDudoText.text = "Dammit ! He's wrong !";
                    _looserID = playerDudoID;
                    _winnerID = GetComponent<NextTurn>().PreviousPlayer(playerDudoID);

                }
                _palificoText.enabled = false;
                _isPalifico = false;
            }
            else
            {
                if (bid.Face == 1 && _dicePool[bid.Face - 1] <= bid.Value) // the bet is paco
                {
                    Debug.Log("win");
                    _winnerDudoText.text = "He's damn right !!";
                    _winnerID = playerDudoID;
                    _looserID = GetComponent<NextTurn>().PreviousPlayer(playerDudoID);
                }
                if (_dicePool[0] + _dicePool[bid.Face - 1] <= bid.Value) // -1 because the tab is between [0 - and (5) ]
                {
                    Debug.Log("win");
                    _winnerDudoText.text = "He's damn right !!";
                    _winnerID = playerDudoID;
                    _looserID = GetComponent<NextTurn>().PreviousPlayer(playerDudoID);
                }
                else
                {
                    Debug.Log("loose");
                    _winnerDudoText.text = "Dammit ! He's wrong !";
                    _looserID = playerDudoID;
                    _winnerID = GetComponent<NextTurn>().PreviousPlayer(playerDudoID);

                }
            }
        }
        public void DudoConfirm()
        {

            OnRoundWin?.Invoke(_looserID);
            _poolPanel.SetActive(false);
            _dudoPanel.SetActive(false);
        }
    }
}