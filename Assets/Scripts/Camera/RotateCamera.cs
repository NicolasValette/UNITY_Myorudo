using Myorudo.Game;
using Myorudo.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Myorudo.CameraAction
{
    public class RotateCamera : MonoBehaviour
    {
        [SerializeField]
        private Transform _cameraTransform;
        [SerializeField]
        private List<Transform> _playerPosition;
        [SerializeField]
        private float _rotatingTime = 5f;
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private PostProcessVolume _postProcessVolume;
        private int _playingPlayer = 0;
        private bool _isLookingDice = false;

        private void OnEnable()
        {
            NextTurn.OnPlayerTurn += LookAtThisPlayer;
        }
        private void OnDisable()
        {
            NextTurn.OnPlayerTurn -= LookAtThisPlayer;

        }
        // Start is called before the first frame update
        void Start()
        {

        }
        public void LookAtThisPlayer(int playerID)
        {
            _playingPlayer = playerID;
        }
        // Update is called once per frame
        void Update()
        {
            if (!_playerInput.IsSpacePressed())
            {
                Rotation();
            }
            else
            {
                LookDice();
            }
        }

        private void Rotation()
        {
            _postProcessVolume.enabled = false;
            Quaternion rotation = Quaternion.LookRotation((_playerPosition[_playingPlayer].position - _cameraTransform.position).normalized);
            _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, rotation, Time.deltaTime * _rotatingTime);

        }
        private void LookDice()
        {
            _postProcessVolume.enabled = true;
            Quaternion rotation = Quaternion.LookRotation((_playerPosition[0].position - _cameraTransform.position).normalized);
            _cameraTransform.rotation = Quaternion.Slerp(_cameraTransform.rotation, rotation, Time.deltaTime * _rotatingTime);
        }

    }
}