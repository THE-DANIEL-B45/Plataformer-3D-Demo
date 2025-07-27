using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : Singleton<SoundManager>
{
    public AudioMixer audioMixer;
    bool muted = false;

    public List<MusicSetup> musicSetups;
    public List<SFXSetup> sfxSetups;

    public AudioSource musicSource;
    public AudioSource sfxSource;

    public void PlayMusicByType(MusicType musicType)
    {
        var music = GetMusicByType(musicType);
        musicSource.clip = music.audioClip;
        musicSource.Play();
    }

    public MusicSetup GetMusicByType(MusicType musicType)
    {
        return musicSetups.Find(i => i.musicType == musicType);
    }

    public void PlaySFXByType(SFXType sfxType)
    {
        var sfx = GetSFXByType(sfxType);
        sfxSource.clip = sfx.audioClip;
        sfxSource.Play();
    }

    public SFXSetup GetSFXByType(SFXType sfxType)
    {
        return sfxSetups.Find(i => i.sfxType == sfxType);
    }

    public void ToogleMasterVolume()
    {
        float targetVolume;

        if (muted) targetVolume = 0;
        else targetVolume = -80f;

        audioMixer.SetFloat("MasterVolume", targetVolume);
        muted = !muted;
    }
}

[System.Serializable]
public enum MusicType
{
    Type_01,
    Type_02,
    Type_03,
}

[System.Serializable]
public enum SFXType
{
    None,
    Type_01,
    Type_02,
    Type_03,
    Type_04
}

[System.Serializable]
public class MusicSetup
{
    public MusicType musicType;
    public AudioClip audioClip;
}

[System.Serializable]
public class SFXSetup
{
    public SFXType sfxType;
    public AudioClip audioClip;
}
