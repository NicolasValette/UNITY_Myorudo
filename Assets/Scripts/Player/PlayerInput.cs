using Myorudo.FSM;
using Myorudo.FSM.States;
using Myorudo.Interfaces.Actions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Myorudo.Player
{
    public class PlayerInput : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField]
        private bool _isDebug = false;
        [SerializeField]
        private GameObject _handFSM;
        #endregion

        private IRoll _rollProvider;

        // Start is called before the first frame update
        void Start()
        {
            if (_handFSM == null)
            {
                Debug.LogError($"Missing component handFSM in {gameObject.name}");
            }
            else
            {
                _rollProvider = _handFSM.GetComponent<IRoll>();
                if (_rollProvider == null)
                {
                    Debug.LogError($"Missing component in {gameObject.name}");
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnBet(InputValue inputValue)
        {
            if (_handFSM.GetComponent<HandFSM>().CurrentState is WaitState)
            {
                if (_isDebug) Debug.Log("OnBet");
                _rollProvider.Bet();
            }
        }
        public void OnLook(InputValue inputValue)
        {
            if (_handFSM.GetComponent<HandFSM>().CurrentState is WaitState)
            {
                if (_isDebug) Debug.Log("OnLook");
                _rollProvider.Look();
            }
        }
        public void OnEndRound(InputValue inputValue)
        {
            if (_handFSM.GetComponent<HandFSM>().CurrentState is BetState)
            {
                if (_isDebug) Debug.Log("OnRoundOver");
                _rollProvider.RoundOver();
            }
        }
        public void OnRoll(InputValue inputValue)
        {
            if (_handFSM.GetComponent<HandFSM>().CurrentState is HoldDiceState)
            {
                if (_isDebug) Debug.Log("OnRoll");
                _rollProvider.Roll();
            }
        }
        public void OnDudo(InputValue inputValue)
        {
            if (_handFSM.GetComponent<HandFSM>().CurrentState is WaitState)
            {
                if (_isDebug) Debug.Log("OnDudo");
                _rollProvider.Dudo();
            }
        }
        public void TakeDice()
        {
            if (_handFSM.GetComponent<HandFSM>().CurrentState is IdleState)
            {
                _rollProvider.TakeDice();
                if (_isDebug) Debug.Log("Take Dice");
            }
        }
    }
}