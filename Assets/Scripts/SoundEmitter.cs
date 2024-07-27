using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip defaultClip;  
    public bool loop = false;
    public bool soundComplete = false;
    public bool phoneMessage = false;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1.0f;  
        audioSource.clip = defaultClip;
        audioSource.loop = loop;
    }

    private void Update()
    {
        if (phoneMessage)
        {
            if (!audioSource.isPlaying && !audioSource.loop && !soundComplete)
            {
                soundComplete = true; 
                EventManager.Instance.CompleteObjective();
            }
        }
        
    }

    public void PlayEmitterSound()
    {
        SoundManager.Instance.PlaySound(defaultClip, audioSource,loop);
    }
    
    public void StopSound()
    {
        SoundManager.Instance.StopSound(audioSource);
    }
}
