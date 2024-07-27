using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    public AudioSource defaultAudioSource;
    public AudioMixer audioMixer;
    public AudioClip objectiveCompleteSound;
    public AudioClip newObjectiveSound;
    public AudioClip[] bookSounds;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip clip, AudioSource audioSource, bool loop = false)
    {
        if (audioSource.isPlaying && audioSource.clip == clip)
        {
            return;  
        }

        audioSource.clip = clip;
        audioSource.loop = loop;
        audioSource.Play();
    }

    public void StopSound(AudioSource audioSource)
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    public void PlayObjectiveCompleteSound()
    {
        PlaySound(objectiveCompleteSound, defaultAudioSource);
    }

    public void PlayNewObjectiveSound()
    {
        PlaySound(newObjectiveSound,defaultAudioSource);
    }
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(volume) * 20);  
    }

    public void SetMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(volume) * 20);
    }
    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);  
    }

    public void SetUIVolume(float volume)
    {
        audioMixer.SetFloat("UIVolume", Mathf.Log10(volume) * 20);
    }
    public void PlayBookSound()
    {
        int index = Random.Range(0, bookSounds.Length);
        PlaySound(bookSounds[index],defaultAudioSource);
    }
}
