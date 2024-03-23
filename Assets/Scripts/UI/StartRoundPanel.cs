using Myorudo.DebugTools;
using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.UI
{
    public class StartRoundPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject _startPanel;


        private void OnEnable()
        {
            NextTurn.PrepareNextRound += DisplayPanel;
        }
        private void OnDisable()
        {
            NextTurn.PrepareNextRound -= DisplayPanel;
        }

        // Start is called before the first frame update
        void Start()
        {
            _startPanel.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void DisplayPanel()
        {
            _startPanel.SetActive(true);
        }
        public void HidePanel()
        {
            _startPanel.SetActive(false);
        }
    }
}