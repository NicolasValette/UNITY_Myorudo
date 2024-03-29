using Myorudo.Audio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Myorudo.UI
{
    public class StartMenu : MonoBehaviour
    {
        [SerializeField]
        private AudioPlayer _audioPlayer;
        [SerializeField]
        private Slider _masterSlider;
        [SerializeField]
        private Slider _musicSlider;
        [SerializeField]
        private Slider _sfxSlider;
        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {

        }
        public void ShowOption()
        {
            _masterSlider.value = _audioPlayer.MasterVolume;
            _musicSlider.value = _audioPlayer.MusicVolume;
            _sfxSlider.value = _audioPlayer.SFXVolume;
        }
    }
}