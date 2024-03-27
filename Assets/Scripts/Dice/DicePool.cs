using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Dice
{
    public class DicePool : MonoBehaviour
    {

        [SerializeField]
        private GameObject _pool;
        [SerializeField]
        private List<GameObject> _dices;
        [SerializeField]
        private List<Vector3> _rotationAngle;
        [SerializeField]
        private NextTurn _TurnManager;
        [SerializeField]
        private int _playerID;

        private void OnEnable()
        {
            DudoHandler.OnDudoRevealHand += Reveal;
            NextTurn.OnFirstRoundStart += InitDice;
        }
        private void OnDisable()
        {
            DudoHandler.OnDudoRevealHand -= Reveal;
            NextTurn.OnFirstRoundStart -= InitDice;
        }

        // Start is called before the first frame update
        void Start()
        {
           
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitDice()
        {
            for (int i = 0; i < _dices.Count; i++)
            {
                _dices[i].SetActive(false);
            }
            if (!_TurnManager.PlayerList[_playerID].IsEliminated)
            {
                for (int i = 0; i < _TurnManager.PlayerList[_playerID].DiceResult.Count; i++)
                {
                    _dices[i].SetActive(true);
                    _dices[i].transform.rotation = Quaternion.Euler(_rotationAngle[_TurnManager.PlayerList[_playerID].DiceResult[i]-1]);
                }
                
            }
        }
        public void Reveal()
        {

        }
    }
}