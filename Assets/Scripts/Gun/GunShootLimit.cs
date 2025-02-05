using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GunShootLimit : GunBase
{
    public List<UIFillUpdater> UIGunUpdaterList;

    public float maxShoot = 5f;
    public float timeToRecharge = 1f;

    protected float _currentShots;
    protected bool _recharging = false;

    private void Awake()
    {
        GetAllUis();
    }

    protected override IEnumerator ShootCoroutine()
    {
        if(_recharging) yield break;

        while (true)
        {
            if(_currentShots < maxShoot)
            {
                Shoot();
                _currentShots++;
                CheckRecharge();
                UpdateUI();
                yield return new WaitForSeconds(timeBetweenShoot);
            }
        }
    }

    protected void CheckRecharge()
    {
        if(_currentShots>= maxShoot)
        {
            StopShoot();
            StartRecharge();
        }
    }

    protected void StartRecharge()
    {
        _recharging = true;
        StartCoroutine(RechargeCoroutine());
    }

    protected IEnumerator RechargeCoroutine()
    {
        float time = 0;
        while(time< timeToRecharge)
        {
            time += Time.deltaTime;
            UIGunUpdaterList.ForEach(i => i.UpdateValue(time/timeToRecharge));
            yield return new WaitForEndOfFrame();
        }
        _currentShots = 0;
        _recharging = false;
    }

    protected void UpdateUI()
    {
        UIGunUpdaterList.ForEach(i => i.UpdateValue(maxShoot, _currentShots));
    }

    protected void GetAllUis()
    {
        List<UIFillUpdater> UIList = GameObject.FindObjectsOfType<UIFillUpdater>().ToList();

        foreach(UIFillUpdater i in UIList)
        {
            if(i.CompareTag("Bullets"))
            {
                UIGunUpdaterList.Add(i);
                break;
            }
        }
    }
}
