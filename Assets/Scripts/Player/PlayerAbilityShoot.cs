using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAbilityShoot : PlayerAbilityBase
{
    public GunBase gun1;
    public GunBase gun2;
    public Transform gunPosition;
    private GunBase _currentGun;
    public FlashColor _flashColor;

    protected override void Init()
    {
        base.Init();

        ChangeGun(gun1);

        inputs.Gameplay.Shoot.performed += ctx => StartShoot();
        inputs.Gameplay.Shoot.canceled += ctx => CancelShoot();

        inputs.Gameplay.Gun1.performed += ctx => ChangeGun(gun1);
        inputs.Gameplay.Gun2.performed += ctx => ChangeGun(gun2);
    }

    private void ChangeGun(GunBase gun)
    {
        if(_currentGun != null) Destroy(_currentGun.gameObject);

        _currentGun = Instantiate(gun, gunPosition);

        _currentGun.transform.localPosition = _currentGun.transform.localEulerAngles = Vector3.zero;
    }

    private void StartShoot()
    {
        _currentGun.StartShoot();
        _flashColor.Flash();
        Debug.Log("StartShoot");
    }

    private void CancelShoot()
    {
        Debug.Log("CancelShoot");
        _currentGun.StopShoot();
    }
}
