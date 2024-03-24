using Myorudo.FSM;
using Myorudo.FSM.States;
using Myorudo.Interfaces.Actions;
using System;
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

        #region InputEvent
        public static event Action OnRollReady;
        #endregion

        private IRoll _rollProvider;

        // Start is called before the first frame update
        void Start()
        {
            //if (_handFSM == null)
            //{
            //    Debug.LogError($"Missing component handFSM in {gameObject.name}");
            //}
            //else
            //{
            //    _rollProvider = _handFSM.GetComponent<IRoll>();
            //    if (_rollProvider == null)
            //    {
            //        Debug.LogError($"Missing component in {gameObject.name}");
            //    }
            //}
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.escapeKey.wasPressedThisFrame)
            {
                Debug.Log("escape");
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
            }
        }
        public void OnBet(InputValue inputValue)
        {
            //if (_handFSM.GetComponent<HandFSM>().CurrentState is WaitState)
            //{
            //    if (_isDebug) Debug.Log("OnBet");
            //    _rollProvider.Bet();
            //}
        }
        public void OnLook(InputValue inputValue)
        {
            //if (_handFSM.GetComponent<HandFSM>().CurrentState is WaitState)
            //{
            //    if (_isDebug) Debug.Log("OnLook");
            //    _rollProvider.Look();
            //}
        }
        public void OnEndRound(InputValue inputValue)
        {
            //if (_handFSM.GetComponent<HandFSM>().CurrentState is BetState)
            //{
            //    if (_isDebug) Debug.Log("OnRoundOver");
            //    _rollProvider.RoundOver();
            //}
        }
        public void OnRoll(InputValue inputValue)
        {
            //if (_handFSM.GetComponent<HandFSM>().CurrentState is HoldDiceState)
            //{
            //    if (_isDebug) Debug.Log("OnRoll");
            //    _rollProvider.Roll();
            //}
        }
        public void OnDudo(InputValue inputValue)
        {
            //if (_handFSM.GetComponent<HandFSM>().CurrentState is WaitState)
            //{
            //    if (_isDebug) Debug.Log("OnDudo");
            //    _rollProvider.Dudo();
            //}
        }
        public void TakeDice()
        {
            //if (_handFSM.GetComponent<HandFSM>().CurrentState is IdleState)
            //{
            //    _rollProvider.TakeDice(GetPosition());
            //    if (_isDebug) Debug.Log("Take Dice");
            //}
        }


        public bool IsMouseClick()
        {
            var mouse = Mouse.current;
            return mouse.leftButton.isPressed;
        }
        public Vector3 GetPosition()
        {
            var mouse = Mouse.current;
            Ray rayToMouse = Camera.main.ScreenPointToRay(mouse.position.ReadValue());
            RaycastHit hit;
            if (Physics.Raycast(rayToMouse, out hit))
            {
                Vector3 vect = new Vector3(hit.point.x, hit.point.y, hit.point.z);
                return vect;
            }
            return Vector3.zero;
        }

        public Vector2 GetCursorDeltaPos()
        {
            var mouse = Mouse.current;
            return mouse.delta.ReadValue();
        }
    }
}