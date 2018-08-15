using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Menu : MonoBehaviour
{
    AudioSource audioSource;

    public void Start()
    {
        this.audioSource = GetComponent<AudioSource>();
    }

    public void OnOffAudio()
    {
        if (this.audioSource.mute == true)
            this.audioSource.mute = false;
        else
            this.audioSource.mute = true;
    }
}
