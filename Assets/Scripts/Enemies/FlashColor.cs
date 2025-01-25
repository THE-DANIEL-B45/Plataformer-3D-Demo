using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashColor : MonoBehaviour
{
    public MeshRenderer meshRenderer;

    [Header("Setup")]
    public Color color = Color.red;
    public float duration = 0.1f;

    private Color defaultColor;

    private Tween _currTween;

    private void Start()
    {
        defaultColor = meshRenderer.material.GetColor("_EmissionColor");
    }

    public void Flash()
    {
        if(!_currTween.IsActive())
        meshRenderer.material.DOColor(color, "_EmissionColor", duration).SetLoops(2, LoopType.Yoyo);
    }
}
