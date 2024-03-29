using Myorudo.Datas;
using Myorudo.FSM;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Myorudo.UI
{
    public class RollPanel : MonoBehaviour
    {
        [SerializeField]
        private DiceData _diceData;
        [SerializeField]
        private GameObject _playerFSMGameObject;
        [SerializeField]
        private GameObject _resultPanel;
        [SerializeField]
        private GameObject _resultImagePrefab;
        [SerializeField]
        private GameObject _rollButton;
        [SerializeField]
        private GameObject _confirmButton;
        [SerializeField]
        private HumanPlayerFSM _playerFSM;
        private List<int> _diceResult;

        private void Awake()
        {
            _resultPanel.SetActive(false);
            _diceResult = new List<int>();
            _rollButton.SetActive(false);
            _confirmButton.SetActive(false);
        }
        private void OnEnable()
        {
            _playerFSM.OnRollFinished += RetrieveResult;
            NextTurn.RollDice += ToggleDiplayPanel;
        }
        private void OnDisable()
        {
            _playerFSM.OnRollFinished -= RetrieveResult;
            NextTurn.RollDice -= ToggleDiplayPanel;
        }
        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void ToggleDiplayPanel()
        {
            _rollButton.SetActive(!_rollButton.activeSelf);
        }
        public void RetrieveResult(List<int> diceResult)
        {
            _resultPanel.SetActive(true);
            _diceResult = diceResult;
            StringBuilder strb = new StringBuilder();
            _confirmButton.SetActive(true);
           
            for (int i=0; i<_diceResult.Count; i++)
            {
                GameObject diceImg = Instantiate(_resultImagePrefab);
                diceImg.GetComponent<Image>().sprite = _diceData[_diceResult[i]];
                diceImg.transform.SetParent(_resultPanel.transform, false);
                strb.Append($" - {_diceResult[i]}");
            }
            Debug.Log(strb.ToString());
            
        }
        public void ManualRollDice()
        {
            _playerFSM.PrepareToManualRoll();
        }
        public void RandomRollDice()
        {
            _playerFSM.PrepareToRandomRoll();
        }
        public void ConfirmRoll()
        {
            _playerFSM.EndRoll();
            for (int i=0; i < _resultPanel.transform.childCount;i++)
            {
                Destroy(_resultPanel.transform.GetChild(i).gameObject);

            }
            _resultPanel.SetActive(false);
        }
    }
}