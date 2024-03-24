using Myorudo.Interfaces.Dice;
using Myorudo.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Myorudo.Dice
{
    [RequireComponent(typeof(MoveDice))]
    public class ManualRoll : MonoBehaviour
    {
        [SerializeField]
        private Player.PlayerInput _playerInput;
        private IMoveDice _moveDiceProvider;
        private bool _isDiceInHand = false;
        // Start is called before the first frame update
        void Start()
        {
            _moveDiceProvider = GetComponent<IMoveDice>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.aKey.wasPressedThisFrame)
            {
                Debug.Log("TakeDice");
                _moveDiceProvider.TakeDice(_playerInput.GetPosition());
                _isDiceInHand=true;
            }

            if (_isDiceInHand)
            {
                _moveDiceProvider.MoveHeldDice(_playerInput.GetPosition());
                
                if (_playerInput.IsMouseClick())
                {
                    _moveDiceProvider.Roll(_playerInput.GetCursorDeltaPos());
                    _isDiceInHand = false;
                }
            }
        }
    }
}