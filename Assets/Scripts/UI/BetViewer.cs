using Myorudo.Actions;
using Myorudo.Datas;
using Myorudo.FSM;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;


namespace Myorudo.UI
{
    public class BetViewer : MonoBehaviour
    {
        [SerializeField]
        private GameObject _gameObjectPanel;
        [SerializeField]
        private TMP_Text _betPreviewPanel;
        [SerializeField]
        private string _defaultText = "No one make a bet yet !";

        private void OnEnable()
        {
            NextTurn.OnFirstRoundStart += DisplayBetViewerPanel;
            Bet.OnBet += UpdateBetText;
            DudoHandler.OnDudoRevealHand += HideDisplayBetViewerPanel;
        }
        private void OnDisable()
        {
            NextTurn.OnFirstRoundStart -= DisplayBetViewerPanel;
            Bet.OnBet -= UpdateBetText;
            DudoHandler.OnDudoRevealHand -= HideDisplayBetViewerPanel;
        }

        // Start is called before the first frame update
        void Start()
        {
            _betPreviewPanel.text = _defaultText;
            _gameObjectPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void UpdateBetText(int playerID, Bid bid)
        {
            _betPreviewPanel.text = $"Player #{playerID} bet {bid.Value} dices on {bid.Face} !";
        }
        public void DisplayBetViewerPanel()
        {
            _gameObjectPanel.SetActive(true);
        }
        public void HideDisplayBetViewerPanel()
        {
            _betPreviewPanel.text = _defaultText;
            _gameObjectPanel.SetActive(false);
        }
    }
}