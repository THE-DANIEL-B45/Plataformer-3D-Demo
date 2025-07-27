using Ebac.Core.Singleton;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXPool : Singleton<SFXPool>
{
    public int poolSize = 10;
    private List<AudioSource> audioSourceList;
    private int index = 0;

    private void Start()
    {
        CreatePool();
    }

    private void CreatePool()
    {
        audioSourceList = new List<AudioSource>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSourceItem();
        }
    }

    private void CreateAudioSourceItem()
    {
        GameObject go = new GameObject("SFX_Pool");
        go.transform.SetParent(gameObject.transform);
        audioSourceList.Add(go.AddComponent<AudioSource>());
    }

    public void Play(SFXType sfxType)
    {
        if (sfxType == SFXType.None) return;

        var sfx = SoundManager.Instance.GetSFXByType(sfxType);
        audioSourceList[index].clip = sfx.audioClip;
        audioSourceList[index].Play();

        index++;
        if(index >= audioSourceList.Count) index = 0;
    }
}
