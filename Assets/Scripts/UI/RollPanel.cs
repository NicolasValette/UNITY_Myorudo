using Myorudo.FSM;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

namespace Myorudo.UI
{
    public class RollPanel : MonoBehaviour
    {

        [SerializeField]
        private GameObject _playerFSMGameObject;
        [SerializeField]
        private TMP_Text _resultPanel;
        [SerializeField]
        private GameObject _rollButton;
        [SerializeField]
        private GameObject _confirmButton;
        [SerializeField]
        private PlayerSFM _playerFSM;
        private List<int> _diceResult;

        private void Awake()
        {
            _resultPanel.enabled = false;
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
            _resultPanel.enabled = true;
            _diceResult = diceResult;
            StringBuilder strb = new StringBuilder();
            strb.Append(_diceResult[0]);
            for (int i=1; i<_diceResult.Count; i++)
            {
                strb.Append($" - {_diceResult[i]}");
            }
            _resultPanel.text = strb.ToString();
            _confirmButton.SetActive(true);
            
        }
        public void RollDice()
        {
            _playerFSM.PrepareToRoll();
        }
        public void ConfirmRoll()
        {
            _playerFSM.EndRoll();
            _resultPanel.enabled = false;
        }
    }
}