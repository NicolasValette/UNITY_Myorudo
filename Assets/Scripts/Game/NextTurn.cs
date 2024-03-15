using Myorudo.Datas;
using Myorudo.FSM.States.PlayerState;
using Myorudo.Interfaces.Game;
using System;
using System.Collections;
using System.Collections.Generic;
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

        #region EVENTS
        public static event Action RollDice;
        #endregion

        private List<IPlay> _playerSMFList;
        private int _numberOfFinishRoll = 0;
        private int _nextPlayer = 0;
        // Start is called before the first frame update
        void Start()
        {
            _playerSMFList = new List<IPlay>();
            for (int i = 0; i< _playerList.Count; i++)
            {
                _playerSMFList.Add(_playerList[i].GetComponent<IPlay>());
            }
            for (int i = _playerList.Count; i< _gameRulesData.NumberOfPlayer;i++)
            {
                GameObject tmpGO = Instantiate(_IAPlayerPrefab);
                _playerSMFList.Add(tmpGO.GetComponent<IPlay>());
            }
            _nextPlayer = UnityEngine.Random.Range(0, _gameRulesData.NumberOfPlayer);
        }
        // Update is called once per frame
        void Update()
        {

        }

        public void WaitEveryoneRoll()
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
            for (int i=0; i < _playerSMFList.Count; i++) 
            {
                _playerSMFList[i].PrepareToStart(i);
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
    }
}