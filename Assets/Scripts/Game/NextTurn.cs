using Myorudo.Datas;
using Myorudo.FSM;
using Myorudo.FSM.States.PlayerState;
using Myorudo.Interfaces.Game;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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


        #region EVENTS
        public static event Action RollDice;
        #endregion

        private List<IPlay> _playerSMFList;
        private int _numberOfFinishRoll = 0;
        private int _nextPlayer = 0;

        #region Debug attribute
        private List<PlayerSFM> _debugPlayerSFMList;
        #endregion
        private void OnDisable()
        {
            
        }

        // Start is called before the first frame update
        void Start()
        {
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
            for (int i = 0; i< _playerList.Count; i++)
            {
                _playerSMFList.Add(_playerList[i].GetComponent<IPlay>());
            }
            for (int i = _playerList.Count; i< _gameRulesData.NumberOfPlayer;i++)
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
                for (int i=0; i < _debugPlayerSFMList.Count; i++)
                {
                    string name = _debugPlayerSFMList[i].CurrentState.GetType().Name;
                    _debugStatesTMP[i].text = $"Player {_debugPlayerSFMList[i].PlayerId} : {name}";

                }
            }
        }

        public void WaitEveryoneRoll(List<int> dices)
        {
            _numberOfFinishRoll++;
            Debug.Log("Roll finished : " + _numberOfFinishRoll);
            if (_numberOfFinishRoll >= _gameRulesData.NumberOfPlayer)
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

            for (int i=0; i < _playerSMFList.Count; i++) 
            {
                _playerSMFList[i].PrepareToStart(i, dudoProvider);
                _playerSMFList[i].OnTurnOver += NextPlayer;
                _playerSMFList[i].OnRollFinished += WaitEveryoneRoll;
            }
            _nextPlayer = UnityEngine.Random.Range(0, _gameRulesData.NumberOfPlayer);
            RollDice?.Invoke();
        }
        public void NextPlayer()
        {
            _nextPlayer = (_nextPlayer + 1) % _gameRulesData.NumberOfPlayer;
            _playerSMFList[_nextPlayer].Play();
        }
        public void RoundOver(int looserId)
        {
            _playerSMFList[looserId].LooseDices(_gameRulesData.NumberOfDiceLost);
            for (int i = 0; i< _playerSMFList.Count-1; i++)
            {
                _playerSMFList[i].FinishRound();
            }
        }
    }
}