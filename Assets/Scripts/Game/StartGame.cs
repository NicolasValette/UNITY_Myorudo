using Myorudo.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Game
{
    public class StartGame : MonoBehaviour
    {
        [SerializeField]
        private StartRoundPanel _startRoundPanel;
        [SerializeField]
        private NextTurn _turnHandler;


        private void OnEnable()
        {
            
        }
        // Start is called before the first frame update
        void Start()
        {
            _startRoundPanel.DisplayPanel();
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void StartingGame()
        {
            _turnHandler.StartGame();
        }
        public void NewRound()
        {
            _turnHandler.StartRound();
        }
    }
}