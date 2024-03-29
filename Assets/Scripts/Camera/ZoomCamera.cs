using Myorudo.Game;
using Myorudo.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Myorudo.CameraAction
{


    public class ZoomCamera : MonoBehaviour
    {
        [SerializeField]
        private Camera _camera;
        [SerializeField]
        private float _zoomDuration = 0.5f;
        [SerializeField]
        private PlayerInput _playerInput;

        private float _defaultfov;
        private bool _isZoom;
        private bool _isReveal = false;

        private void OnEnable()
        {
            DudoHandler.OnDudoRevealHand += RevealDice;
            DudoHandler.OnRoundWin += UnrevealDice;
        }
        private void OnDisable()
        {
            DudoHandler.OnDudoRevealHand -= RevealDice;
            DudoHandler.OnRoundWin -= UnrevealDice;
        }
        // Start is called before the first frame update
        void Start()
        {
            _defaultfov = _camera.fieldOfView;
        }

        // Update is called once per frame
        void Update()
        {
            if (!_isReveal)
            {
                if (_playerInput.IsSpacePressed())
                {
                    if (!_isZoom)
                    {
                        Zoom();
                    }
                }
                else
                {
                    if (_isZoom)
                    {
                        Unzoom();
                    }
                }
            }
        }
        private void Zoom()
        {
            if (!_isZoom)
            {

                StartCoroutine(LerpZoom(_defaultfov / 2f, _zoomDuration));
                _isZoom = true;
            }
        }
        private void Unzoom()
        {
            if (_isZoom)
            {

                StartCoroutine(LerpZoom(_defaultfov, _zoomDuration));
                _isZoom = false;
            }
        }

        private void RevealDice()
        {
            Zoom();
            _isReveal = true;
        }
        public void UnrevealDice(int winner)
        {
            Unzoom();
            _isReveal = false;
        }


        IEnumerator LerpZoom(float endValue, float duration)
        {
            float time = 0;
            float startValue = _camera.fieldOfView;

            while (time < duration)
            {
                _camera.fieldOfView = Mathf.Lerp(startValue, endValue, time / duration);
                time += Time.deltaTime;
                yield return null;
            }
            _camera.fieldOfView = endValue;
        }
    }
}