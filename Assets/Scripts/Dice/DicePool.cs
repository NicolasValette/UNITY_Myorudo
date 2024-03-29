using Myorudo.Game;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Myorudo.Dice
{
    public class DicePool : MonoBehaviour
    {

        [SerializeField]
        protected GameObject _pool;
        [SerializeField]
        protected List<GameObject> _dices;
        [SerializeField]
        protected List<Vector3> _rotationAngle;
        [SerializeField]
        protected NextTurn _TurnManager;
        [SerializeField]
        protected int _playerID;

        protected bool _isReveal = false;
        void OnEnable()
        {
            DudoHandler.OnDudoRevealHand += Reveal;
            DudoHandler.OnRoundWin += Unreveal;
            NextTurn.OnFirstRoundStart += InitDice;
        }
        void OnDisable()
        {
            DudoHandler.OnDudoRevealHand -= Reveal;
            DudoHandler.OnRoundWin -= Unreveal;
            NextTurn.OnFirstRoundStart -= InitDice;
        }

        // Start is called before the first frame update
        void Start()
        {
            _pool.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void InitDice()
        {
           
            _pool.SetActive(false);
            for (int i = 0; i < _dices.Count; i++)
            {
                _dices[i].SetActive(false);
            }
            if (!_TurnManager.GetPlayer(_playerID).IsEliminated)
            {
                for (int i = 0; i < _TurnManager.GetPlayer(_playerID).DiceResult.Count; i++)
                {
                    _dices[i].SetActive(true);
                    _dices[i].transform.rotation = Quaternion.Euler(_rotationAngle[_TurnManager.GetPlayer(_playerID).DiceResult[i]-1]);
                }
                
            }
        }
        public virtual void Reveal()
        {
            _isReveal = true;
            _pool.SetActive(true);
        }
        public virtual void Unreveal(int id)
        {
            _isReveal = false;
            _pool.SetActive(false);
        }
    }
}