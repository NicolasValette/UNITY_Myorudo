using Myorudo.Datas;
using Myorudo.FSM;
using Myorudo.FSM.States.PlayerState;
using Myorudo.Interfaces.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Myorudo.Game
{
    public class NextTurn : MonoBehaviour
    {
        [SerializeField]
        private GameRulesData _gameRulesData;
        [SerializeField]
        private List<GameObject> _playerList;
        [SerializeField]
        private GameObject _IAPlayerPrefab;
        [Header("Debug")]
        [SerializeField]
        private bool _isDebugMode = false;
        [SerializeField]
        private List<TMP_Text> _debugStatesTMP;
        [SerializeField]
        private TMP_Text _roundNumberText;


        #region EVENTS
        public static event Action RollDice;
        public static event Action PrepareNextRound;
        public static event Action<bool> OnGameEnd;
        public static event Action OnGameInit;
        #endregion

        private List<IPlay> _playerSMFList;
        public List<IPlay> PlayerList { get => _playerSMFList; }
        private Dictionary<int, IPlay> _playerSMFDictionary;
        private int _numberOfFinishRoll = 0;
        private static int _nextPlayer = 0;
        public static int CurrentPlayer => _nextPlayer;
        private static int _roundNumber = 0;
        public static int RoundNumber { get => _roundNumber; }

        #region Debug attribute
        private List<PlayerSFM> _debugPlayerSFMList;
        #endregion

        private int _numberOfEliminated = 0;

        private void OnEnable()
        {
            DudoHandler.OnRoundWin += RoundOver;
        }
        private void OnDisable()
        {
            DudoHandler.OnRoundWin -= RoundOver;
        }

        // Start is called before the first frame update
        void Start()
        {
            _playerSMFDictionary = new Dictionary<int, IPlay>();
            _roundNumberText.enabled = _isDebugMode;
            for (int i = 0; i < _debugStatesTMP.Count; i++)
            {
                _debugStatesTMP[i].enabled = _isDebugMode;
            }
            if (_isDebugMode)
            {
                _debugPlayerSFMList = new List<PlayerSFM>
                {
                    _playerList[0].GetComponent<PlayerSFM>()
                };
            }

            _playerSMFList = new List<IPlay>();
            for (int i = 0; i < _playerList.Count; i++)
            {
                _playerSMFList.Add(_playerList[i].GetComponent<IPlay>());
              
            }
            for (int i = _playerList.Count; i < _gameRulesData.NumberOfPlayer; i++)
            {
                GameObject tmpGO = Instantiate(_IAPlayerPrefab);
                _playerList.Add(tmpGO);
                _playerSMFList.Add(tmpGO.GetComponent<IPlay>());
                _debugPlayerSFMList.Add(tmpGO.GetComponent<PlayerSFM>());
            }
            _nextPlayer = UnityEngine.Random.Range(0, _gameRulesData.NumberOfPlayer);



        }
        // Update is called once per frame
        void Update()
        {
            if (_isDebugMode)
            {
                
                for (int i = 0; i < _debugPlayerSFMList.Count; i++)
                {
                    string name = _debugPlayerSFMList[i].CurrentState.GetType().Name;
                    _debugStatesTMP[i].text = $"Player #{_debugPlayerSFMList[i].PlayerId} ({_debugPlayerSFMList[i].NumberOfDiceLeft}) : {name}";

                }
            }
        }

        
        public int PreviousPlayer(int initialPlayerID)
        {
            int initTemp = initialPlayerID;
            do
            {
                initTemp--;
                initTemp = (initTemp) < 0 ? _gameRulesData.NumberOfPlayer - 1 : initTemp;
            } while (_playerSMFDictionary[initTemp].IsEliminated);
            return initTemp;
        }
    
        public void RemovePlayer(int playerId)
        {
            if (_playerSMFDictionary[playerId].Type == PlayerType.Human)
            {
                OnGameEnd?.Invoke(false);       // we raise event, false = loose
            }
            _playerSMFList.RemoveAt(playerId);
            //for (int i = playerId; i < _playerSMFList.Count; i++)
            //{
            //    _playerSMFList[i].DecreaseId();
            //}
            //_playerSMFList.RemoveAt(playerId);
            //_playerList.RemoveAt(playerId);
            _numberOfEliminated++;
            _playerSMFDictionary[playerId].Eliminate();
            if (_nextPlayer == playerId)
            {
                _nextPlayer = GetNextPlayer();
            }
        }

        public void WaitEveryoneRoll()
        {
            _numberOfFinishRoll++;
            Debug.Log("Roll finished : " + _numberOfFinishRoll);
            if (_numberOfFinishRoll >= _gameRulesData.NumberOfPlayer - _numberOfEliminated)
            {
                NextPlayer();
            }
        }
        public void StartGame()
        {
            var dudoProvider = GetComponent<DudoHandler>();
            if (dudoProvider == null)
            {
                Debug.LogError($"Missing componang DudoHandler in {gameObject.name}");
                dudoProvider = gameObject.AddComponent<DudoHandler>();
            }

            for (int i = 0; i < _playerSMFList.Count; i++)
            {
                _playerSMFList[i].PrepareToStart(i, dudoProvider);
                _playerSMFDictionary.Add(i, _playerSMFList[i]);
                _playerSMFList[i].OnTurnOver += NextPlayer;
                _playerSMFList[i].OnRollConfirmed += WaitEveryoneRoll;
            }
            _nextPlayer = UnityEngine.Random.Range(0, _gameRulesData.NumberOfPlayer);
            _nextPlayer = 1;
            OnGameInit?.Invoke();
            _numberOfEliminated = 0;
            Debug.Log($"First player is Player {(_nextPlayer + 1) % _gameRulesData.NumberOfPlayer}");
            StartRound();
        }
        public void StartRound()
        {

            _roundNumber++;
            _numberOfFinishRoll = 0;
            RollDice?.Invoke();
            if (_isDebugMode) _roundNumberText.text = $"Round Number : {_roundNumber}";
        }
        public void NextPlayer()
        {
            Debug.Log($"Turn : Player #{_nextPlayer} ");
            _playerSMFDictionary[_nextPlayer].Play();


            _nextPlayer = GetNextPlayer();
          

        }
        private int GetNextPlayer()
        {
            int next = _nextPlayer;
            do
            {
                next = (next + 1) % _gameRulesData.NumberOfPlayer;
            } while (_playerSMFDictionary[next].IsEliminated);
            return next;
        }
        public void RoundOver(int looserId)
        {
            if (_playerSMFDictionary[looserId].LooseDices(_gameRulesData.NumberOfDiceLost))
            {
                //1 eliminated
               
                RemovePlayer(looserId);
                if (_numberOfEliminated == _gameRulesData.NumberOfPlayer - 1)
                {
                    //WIN
                    Debug.Log("Game win !!!!!!!!!!!!!!!");
                    OnGameEnd?.Invoke(true); // Win
                }
            }
            else
            {
                _nextPlayer = looserId;

            }

            //everybody still in game
            for (int i = 0; i < _playerSMFDictionary.Count; i++)
            {
                _playerSMFDictionary[i].FinishRound();
            }
            PrepareNextRound?.Invoke();

        }
    }
}