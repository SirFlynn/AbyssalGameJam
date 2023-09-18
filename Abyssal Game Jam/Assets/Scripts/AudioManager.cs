using System.Collections;
using UnityEngine.Audio;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource MusicAudio;
    public AudioSource SFXAudio;

    public AudioMixer audioMixer;

    [Range(-80, 0)]
    public float minVolDB = -80f;
    [Range(-15, 15)]
    public float maxVolDB = 0f;

    public static AudioManager Instance { get; private set; }
    [Space]
    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameplayMusic;
    [Space]
    [SerializeField] AudioClip gameButton_1;
    [SerializeField] AudioClip gameButton_2;
    [Space]
    [SerializeField] AudioClip possessSFX;
    [SerializeField] AudioClip holySFX;
    [SerializeField] AudioClip runAwaySFX;
    [SerializeField] AudioClip grandPianoSFX;
    [SerializeField] AudioClip tap;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }
    }

    public void PlayMainMenuMusic()
    {
        MusicAudio.clip = mainMenuMusic;
        MusicAudio.Play();
        MusicAudio.loop = true;
    }
    public void PlayGameplayMusic()
    {
        MusicAudio.clip = gameplayMusic;
        MusicAudio.Play();
        MusicAudio.loop = true;
    }
    public void PlayGameButton_1SFX()
    {
        SFXAudio.clip = gameButton_1;
        SFXAudio.Play();
    }
    public void PlayGameButton_2SFX()
    {
        SFXAudio.clip = gameButton_2;
        SFXAudio.Play();
    }
    public void PlayPossessing()
    {
        SFXAudio.clip = possessSFX;
        SFXAudio.Play();
    }

    public void PlayHolySFX()
    {
        SFXAudio.clip = holySFX;
        SFXAudio.Play();
    }

    public void PlayRunAway()
    {
        SFXAudio.clip = runAwaySFX;
        SFXAudio.Play();
    }
    public void PlayGrandPiano()
    {
        SFXAudio.clip = grandPianoSFX;
        SFXAudio.Play();
    }
    public void PlayTap()
    {
        SFXAudio.clip = tap;
        SFXAudio.Play();
    }

}
