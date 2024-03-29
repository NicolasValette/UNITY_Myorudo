using Myorudo.Interfaces.Dice;
using Myorudo.Player;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Myorudo.Dice
{
    [RequireComponent(typeof(MoveDice))]
    public class ManualRoll : MonoBehaviour, IRollDice
    {
        [SerializeField]
        private AudioClip _throwSoundEffect;
        [SerializeField]
        private AudioClip _shuffleSoundEffect;
        [SerializeField]
        private Player.PlayerInput _playerInput;
        [SerializeField]
        private Transform _defaultLaunchPos;
        private IMoveDice _moveDiceProvider;
        private bool _isDiceInHand = false;
        private int _diceReaded = 0;
        private List<int> _diceResult;
        List<GameObject> _dices;
        private PlayAudioEffect _audioEffectPlayer;

        public event Action<List<int>> OnRollResult;

        // Start is called before the first frame update
        void Start()
        {
            _dices = new List<GameObject>();
            _moveDiceProvider = GetComponent<IMoveDice>();
            _audioEffectPlayer = GetComponent<PlayAudioEffect>();
        }

        // Update is called once per frame
        void Update()
        {

            if (_isDiceInHand)
            {
                _moveDiceProvider.MoveHeldDice(_playerInput.GetPosition());
                
                if (_playerInput.IsMouseClick())
                {
                    if (!_playerInput.IsRayHit())
                    {
                        _moveDiceProvider.MoveHeldDice(_defaultLaunchPos.position);
                    }
                    if (_audioEffectPlayer != null)
                    {
                        _audioEffectPlayer.Stop();
                        _audioEffectPlayer.PlayEffect(_throwSoundEffect);
                    }
                    _moveDiceProvider.Roll(_playerInput.GetCursorDeltaPos());
                    _isDiceInHand = false;
                }
            }
        }

        public void RollDice(int nbOfDices)
        {
            _isDiceInHand = true;
            _diceResult = new List<int>();
            _dices.Clear();
            _dices = _moveDiceProvider.TakeDice(_playerInput.GetPosition(), nbOfDices);
            if (_audioEffectPlayer != null)
            {
                _audioEffectPlayer.PlayEffect(_shuffleSoundEffect, true);
            }
            for (int i=0;i<_dices.Count;i++)
            {
                _dices[i].GetComponent<DiceBehaviour>().OnDiceStop += ReadDice;
            }

        }
        public void ReadDice (DiceFace face)
        {
            _diceResult.Add((int)face);

            if (_diceResult.Count >= _dices.Count)
            {
                Debug.Log("Roll finish");
                StringBuilder strb = new StringBuilder();
                for (int i=0; i<_diceResult.Count;i++)
                {
                    strb.Append($"-{_diceResult[i]}");
                }
                strb.Append("-");
                Debug.Log(strb.ToString());
                EraseDice();
                OnRollResult?.Invoke(_diceResult);
            }
        }

        private void EraseDice()
        {
            _isDiceInHand = false;
            for (int i=0;i<_diceResult.Count;i++)
            {
                _dices[i].GetComponent<DiceBehaviour>().OnDiceStop -= ReadDice;

            }
            Destroy(_dices[0].transform.parent.gameObject, 5f);

        }
    }
}