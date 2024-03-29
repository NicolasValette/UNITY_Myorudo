using Myorudo.Actions;
using Myorudo.Datas;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Myorudo.UI
{
    public class PopulateHistoric : MonoBehaviour
    {
        [SerializeField]
        private GameObject _historicPanel;
        [SerializeField]
        private GameObject _linePrefab;
        [SerializeField]
        private GameObject _bidArea;


        private void OnEnable()
        {
            Bet.OnBet += AddBetToHistoric;
            DudoHandler.OnDudoRevealHand += CleanBidArea;
            NextTurn.OnFirstRoundStart += ShowPanel;
        }
        private void OnDisable()
        {
            Bet.OnBet -= AddBetToHistoric;
            DudoHandler.OnDudoRevealHand -= CleanBidArea;
            NextTurn.OnFirstRoundStart -= ShowPanel;
        }
        // Start is called before the first frame update
        void Start()
        {
            _historicPanel.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void CleanBidArea()
        {
            for (int i=0; i < _bidArea.transform.childCount; i++)
            {
                Destroy(_bidArea.transform.GetChild(i).gameObject);
            }
            HidePanel();
        }

        public void AddBetToHistoric(int playerID, Bid bid)
        {
            
            GameObject newLine = Instantiate(_linePrefab);
            newLine.transform.SetParent(_bidArea.transform, false);

            newLine.GetComponent<TMP_Text>().text = $"Player #{playerID} bet {bid.Value} dices on {bid.Face} !";
        }
        public void ShowPanel()
        {
            _historicPanel.SetActive(true);
        }
        public void HidePanel()
        {
            _historicPanel.SetActive(false);
        }
    }
}