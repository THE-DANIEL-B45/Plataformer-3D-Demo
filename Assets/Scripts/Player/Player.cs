using DG.Tweening;
using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.Core.Singleton;
using Cloth;

public class Player : Singleton<Player> //, IDamageable
{
    public List<Collider> colliders;
    public Animator animator;

    public CharacterController characterController;
    public float speed = 1f;
    public float turnSpeed = 1f;
    public float gravity = -9.8f;

    private float vSpeed = 0f;

    public float jumpSpeed = 15f;

    public KeyCode jumpKeyCode = KeyCode.Space;

    [Header("Run Setup")]
    public KeyCode keyRun = KeyCode.LeftShift;
    public float speedRun = 1.5f;

    [Header("Flash")]
    public List<FlashColor> flashColors;


    private bool _alive = true;
    private bool _jumping = false;

    [Header("Life")]
    public HealthBase healthBase;

    [Space]
    public ClothChanger clothChanger;

    public SFXType jumpAudio = SFXType.Type_03;

    private void OnValidate()
    {
        if(healthBase == null) healthBase = GetComponent<HealthBase>();
    }

    protected override void Awake()
    {
        base.Awake();
        OnValidate();

        healthBase.OnDamage += Damage;
        healthBase.OnKill += OnKill;
    }

    private void Start()
    {
        LoadPlayerInfo();
    }

    private void LoadPlayerInfo()
    {
        SaveSetup saveSetup = SaveManager.Instance.Setup;

        healthBase._currentLife = saveSetup.health;
        healthBase.UpdateUI();

        if(saveSetup.clothSetup.clothType != ClothType.Base)
        {
            StartCoroutine(ChangeTextureCoroutine(saveSetup.clothSetup, 10));

            switch(saveSetup.clothSetup.clothType)
            {
                case ClothType.SPEED:

                    ChangeSpeed(50, 10);

                    break;

                case ClothType.Strong:

                    healthBase.ChangeDamageMultiply(0.5f, 10);

                    break;
            }
        }
    }

    private void Update()
    {
        if(_alive)transform.Rotate(0, Input.GetAxis("Horizontal") * turnSpeed * Time.deltaTime, 0);

        var inputAxisVertical = Input.GetAxis("Vertical");
        var speedVector = transform.forward * inputAxisVertical * speed;

        if(characterController.isGrounded)
        {
            if(_jumping)
            {
                _jumping = false;
                animator.SetTrigger("Land");
            }


            vSpeed = 0f;
            if(Input.GetKeyDown(jumpKeyCode))
            {
                vSpeed = jumpSpeed;
                if(!_jumping)
                {
                    _jumping = true;
                    SFXPool.Instance.Play(jumpAudio);
                    animator.SetTrigger("Jump");
                }
            }
        }

        vSpeed -= gravity * Time.deltaTime;
        speedVector.y = vSpeed;


        var isWalking = inputAxisVertical != 0;
        if(isWalking)
        {
            if(Input.GetKey(keyRun))
            {
                speedVector *= speedRun;
                animator.speed = speedRun;
            }
        }

        if(_alive)characterController.Move(speedVector * Time.deltaTime);

        animator.SetBool("Run", inputAxisVertical != 0);
    }

    [Button]
    public void  Respawn()
    {
        if (CheckpointManager.Instance.HasCheckpoint())
        {
            transform.position = CheckpointManager.Instance.GetPositionFromLastCheckpoint();
        }
    }

    #region Life
    private void OnKill(HealthBase h)
    {
        if(_alive)
        {
            _alive = false;
            animator.SetTrigger("Death");
            colliders.ForEach(i => i.enabled = false);

            Invoke(nameof(Revive), 2f);
        }
    }

    private void Revive()
    {
        _alive = true;
        healthBase.ResetLife();
        animator.SetTrigger("Revive");
        Respawn();
        Invoke(nameof(TurnOnColliders), 1f);
    }

    private void TurnOnColliders()
    {
        colliders.ForEach(i => i.enabled = true);
    }

    public void Damage(HealthBase h)
    {
        foreach(var f in flashColors)
        {
            f.Flash();
        }
        EffectsManager.Instance.ChangeVignette();
        ShakeCamera.Instance.Shake();
    }

    public void Damage(float damage, Vector3 dir)
    {
        foreach (var f in flashColors)
        {
            f.Flash();
        }
    }

    #endregion

    public void ChangeSpeed(float speed, float duration)
    {
        StartCoroutine(ChangeSpeedCoroutine(speed, duration));
    }

    IEnumerator ChangeSpeedCoroutine(float localSpeed, float duration)
    {
        var defaultSpeed = speed;
        speed = localSpeed;

        yield return new WaitForSeconds(duration);

        speed = defaultSpeed;
    }

    public void ChangeTexture(ClothSetup setup, float duration)
    {
        StartCoroutine(ChangeTextureCoroutine(setup, duration));
    }

    IEnumerator ChangeTextureCoroutine(ClothSetup setup, float duration)
    {
        clothChanger.ChangeTexture(setup);

        yield return new WaitForSeconds(duration);

        clothChanger.ResetTexture();
    }
}
