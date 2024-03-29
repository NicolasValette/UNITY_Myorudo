using Myorudo.Audio;
using Myorudo.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myorudo.UI
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _pauseMenu;
        [SerializeField]
        private PlayerInput _playerInput;
        [SerializeField]
        private GameObject _menuPage1;
        [SerializeField]
        private GameObject _menuPage2;
        [SerializeField]
        private Slider _masterSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _sfxSlider;
        private bool _isPaused = false;
        // Start is called before the first frame update
        void Start()
        {
            _pauseMenu.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (_playerInput.WasEscapePressed())
            {
                if (_isPaused)
                {
                    HideMenu();
                }
                else
                {
                    ShowMenu();
                }
            }
        }

        public void ToggleDisplay()
        {

            _pauseMenu.SetActive(!_pauseMenu.activeSelf);

            Time.timeScale = _isPaused ? 1f : 0f;
            _isPaused = !_isPaused;
        }
        public void HideMenu()
        {
            _isPaused = false;
            Time.timeScale = 1f;
            _menuPage1.SetActive(false);
            _menuPage2.SetActive(false);
            _pauseMenu.SetActive(false);
        }
        public void ShowMenu()
        {
            _isPaused = true;
            _menuPage1.SetActive(true);
            _menuPage2.SetActive(false);
            Time.timeScale = 0f;
            _pauseMenu.SetActive(true);
        }
        public void SetSliders()
        {
            _masterSlider.value = AudioPlayer.Instance.MasterVolume;
            _musicSlider.value = AudioPlayer.Instance.MusicVolume;
            _sfxSlider.value = AudioPlayer.Instance.SFXVolume;
        }
        public void SetMasterVolume (float value)
        {
            AudioPlayer.Instance.SetMasterVolume(value);
        }
        public void SetMusicVolume(float value)
        {
            AudioPlayer.Instance.SetMusicVolume(value);
        }
        public void SetSFXVolume(float value)
        {
            AudioPlayer.Instance.SetSFXVolume(value);
        }
    }
}