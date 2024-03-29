using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

namespace Myorudo.Audio
{
    public class AudioPlayer : MonoBehaviour
    {
        
        public enum MixerGroup
        {
            Master,
            Music,
            SFX
        }
        [SerializeField]
        private AudioMixer _audioMixer;

        private static AudioPlayer _instance;
        public static AudioPlayer Instance { get => _instance; }
        public float MasterVolume { get; private set; }
        public float SFXVolume { get; private set; }
        public float MusicVolume { get; private set; }
        private void Awake()
        {

            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                MasterVolume = PlayerPrefs.GetFloat("MasterVolume");
                MusicVolume = PlayerPrefs.GetFloat("MusicVolume");
                SFXVolume = PlayerPrefs.GetFloat("SFXVolume");
               

                DontDestroyOnLoad(this);
            }
           
        }
        // Start is called before the first frame update
        void Start()
        {
            /*   _player.SetVolume(AudioPlayer.MixerGroup.Music, musicVolume);
             _player.SetVolume(AudioPlayer.MixerGroup.Master, masterVolume);
             _musicSlider.value = musicVolume;
             _masterSlider.value = masterVolume;
            */
            SetVolume(MixerGroup.Master, MasterVolume);
            SetVolume(MixerGroup.Music, MusicVolume);
            SetVolume(MixerGroup.SFX, SFXVolume);
        }

        // Update is called once per frame
        void Update()
        {
            if (Keyboard.current.fKey.wasPressedThisFrame)
            {
                Debug.Log("sed");
                SetVolume(MixerGroup.Master,-80f);
            }
        }
        public void SetMasterVolume(float volume)
        {
            Debug.Log("Mastervolume : " + volume);
            SetVolume(MixerGroup.Master, volume);
        }
        public void SetMusicVolume(float volume)
        {
            Debug.Log("Musicvolume : " + volume);
            SetVolume(MixerGroup.Music, volume);
        }
        public void SetSFXVolume(float volume)
        {
            Debug.Log("SFXvolume : " + volume);
            SetVolume(MixerGroup.SFX, volume);
        }

        public void SetVolume(MixerGroup group, float volume)
        {
            string parameter = "";
            if (group == MixerGroup.Master)
            {
                MasterVolume = volume;
                parameter = "MasterVolume";
            }
            else if (group == MixerGroup.Music)
            {
                MusicVolume = volume;
                parameter = "MusicVolume";
            }
            else if (group == MixerGroup.SFX)
            {
                SFXVolume = volume;
                parameter = "SFXVolume";
            }
            _audioMixer.SetFloat(parameter, volume);
            PlayerPrefs.SetFloat(parameter, volume);
        }
    }
}