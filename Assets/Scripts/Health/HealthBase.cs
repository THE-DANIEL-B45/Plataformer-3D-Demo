using Animation;
using Cloth;
using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBase : MonoBehaviour, IDamageable
{
    public float startLife = 10f;
    public bool destroyOnKill = false;
    public float _currentLife;

    public Action<HealthBase> OnDamage;
    public Action<HealthBase> OnKill;

    public List<UIFillUpdater> uIGunUpdater;

    public float damageMultiply = 1f;

    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ResetLife();
    }

    public void ResetLife()
    {
        _currentLife = startLife;
        UpdateUI();
    }

    protected virtual void Kill()
    {
        if(destroyOnKill)
            Destroy(gameObject, 3f);

        OnKill?.Invoke(this);
    }

    [Button]
    public void Damage()
    {
        Damage(5);
    }

    public void Damage(float f)
    {
        _currentLife -= f * damageMultiply;

        if (_currentLife <= 0)
        {
            Kill();
        }

        UpdateUI();
        OnDamage?.Invoke(this);
    }

    public void Damage(float damage, Vector3 dir)
    {
        Damage(damage);
    }

    public void UpdateUI()
    {
        if(uIGunUpdater != null)
        {
            uIGunUpdater.ForEach(i => i.UpdateValue( (float)_currentLife / startLife));
        }
    }

    public void ChangeDamageMultiply(float damage, float duration)
    {
        StartCoroutine(ChangeDamageCoroutine(damage, duration));
    }

    IEnumerator ChangeDamageCoroutine(float damageMultiply, float duration)
    {
        this.damageMultiply = damageMultiply;

        yield return new WaitForSeconds(duration);

        this.damageMultiply = 1;
    }
}
