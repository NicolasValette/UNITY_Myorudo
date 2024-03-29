using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayAudioEffect : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayEffect(AudioClip audioClip, bool isLoop=false)
    {
        AudioSource source = GetComponent<AudioSource>();
        source.clip = audioClip;
        source.loop = isLoop;
        source.Play();
    }
    public void Stop()
    {
        AudioSource source = GetComponent<AudioSource>();
        source.Stop();
    }
}
