using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;
using Ebac.Core.Singleton;

public class EffectsManager : Singleton<EffectsManager>
{
    public PostProcessVolume processVolume;
    [SerializeField] private Vignette vignette;

    public float duration = 1.0f;

    [NaughtyAttributes.Button]
    public void ChangeVignette()
    {
        StartCoroutine(flashColorVignette());
    }

    IEnumerator flashColorVignette()
    {
        Vignette tmp;

        if (processVolume.profile.TryGetSettings<Vignette>(out tmp))
        {
            vignette = tmp;
        }

        ColorParameter c = new ColorParameter();

        float time = 0;

        while (time < duration)
        {
            c.value = Color.Lerp(Color.black, Color.red, time / duration);
            time += Time.deltaTime;
            vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

        time = 0;

        while (time < duration)
        {
            c.value = Color.Lerp(Color.red, Color.black, time / duration);
            time += Time.deltaTime;
            vignette.color.Override(c);
            yield return new WaitForEndOfFrame();
        }

    }
}
