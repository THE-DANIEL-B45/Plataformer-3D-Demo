using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public MusicType musicType;
    public AudioSource audioSource;

    private MusicSetup currentMusicSetup;

    private void Start()
    {
        Play();
    }

    private void Play()
    {
        currentMusicSetup = SoundManager.Instance.GetMusicByType(musicType);

        audioSource.clip = currentMusicSetup.audioClip;
        audioSource.Play();
    }

}
