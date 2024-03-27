using Myorudo.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

        // Start is called before the first frame update
        void Start()
        {
            _defaultfov = _camera.fieldOfView;
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerInput.IsZPressed())
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
        private void Zoom()
        {
            StartCoroutine(LerpZoom(_defaultfov / 2f, _zoomDuration));
            _isZoom = true;
        }
        private void Unzoom()
        {
            StartCoroutine(LerpZoom(_defaultfov, _zoomDuration));
            _isZoom = false;
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