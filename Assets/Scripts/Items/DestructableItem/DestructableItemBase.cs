using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableItemBase : MonoBehaviour
{
    public HealthBase healthBase;

    public float shakeDuration = .1f;
    public int shakeForce = 5;

    public int dropCoinsAmount = 10;

    public GameObject coinPrefab;
    public Transform dropPosition;

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    private void Awake()
    {
        OnValidate();

        healthBase.OnDamage += OnDamage;
    }

    private void OnDamage(HealthBase h)
    {
        transform.DOShakeScale(shakeDuration, Vector3.up/2, shakeForce);
        StartCoroutine(DropGroupOfCoinsCoroutine());
    }

    private void DropCoins()
    {
        var i = Instantiate(coinPrefab);
        i.transform.position = dropPosition.transform.position;
        i.transform.DOScale(0, 1f).SetEase(Ease.OutBack).From();
    }

    IEnumerator DropGroupOfCoinsCoroutine()
    {
        for (int i = 0; i < dropCoinsAmount; i++)
        {
            DropCoins();
            yield return new WaitForSeconds(0.1f);
        }
    }
}
