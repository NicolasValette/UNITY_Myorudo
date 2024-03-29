using Myorudo.Game;
using Myorudo.Player;
using UnityEngine;

namespace Myorudo.Dice
{
    public class DicePoolHuman : DicePool
    {
        [Header("Player attributes")]
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private Transform _waitingPos;
        [SerializeField]
        private Transform _revealPos;

        private bool _activeTurn = false;

        void OnEnable()
        {
            DudoHandler.OnDudoRevealHand += Reveal;
            DudoHandler.OnRoundWin += Unreveal;
            NextTurn.OnFirstRoundStart += InitDice;
            NextTurn.OnPlayerTurn += Turn;
        }
        void OnDisable()
        {
            DudoHandler.OnDudoRevealHand -= Reveal;
            DudoHandler.OnRoundWin -= Unreveal;
            NextTurn.OnFirstRoundStart -= InitDice;
            NextTurn.OnPlayerTurn -= Turn;
        }
        // Start is called before the first frame update
        void Start()
        {
            
            _pool.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerInput.IsSpacePressed() && !_isReveal)
            {
                Show();
            }
            else if (!_isReveal && !_activeTurn) 
            {
                Hide();
            }
        }
        private void Turn(int playerId)
        {
            if (playerId == _playerID)
            {
                Show();
                _activeTurn = true;
            }
            else
            {
                _activeTurn = false;
            }
        }
        private void Show()
        {
            _pool.SetActive(true);
        }
        private void Hide()
        {
            _pool.SetActive(false);
        }
        public override void Reveal()
        {
            base.Reveal();
            _pool.transform.position = _revealPos.position;
        }
        public override void Unreveal(int id)
        {
            base.Unreveal(id);
            _pool.transform.position = _waitingPos.position;
        }
    }
}