using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;
using UnityEngine.Events;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider myCollider;
        public FlashColor flashColor;
        public ParticleSystem myParticleSystem;
        public float startLife = 10f;
        public bool lookAtPlayer = false;

        [SerializeField] private float _currentLife;

        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        [Header("Events")]
        public UnityEvent OnKillEvent;

        private Player _player;

        private void Awake()
        {
            Init();
        }

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
        }

        protected void ResetLife()
        {
            _currentLife = startLife;
            if(startWithBornAnimation) BornAnimation();
        }

        protected virtual void Init()
        {
            ResetLife();
        }

        protected virtual void Kill()
        {
            OnKill();
        }
        protected virtual void OnKill()
        {
            if(myCollider != null) myCollider.enabled = false;
            Destroy(gameObject, 3f);
            PlayAnimationByTrigger(AnimationType.DEATH);
            OnKillEvent?.Invoke();
        }

        public void OnDamage(float f)
        {
            if(flashColor != null) flashColor.Flash();
            if (myParticleSystem != null) myParticleSystem.Emit(15);

            _currentLife -= f;

            if(_currentLife <= 0)
            {
                Kill();
            }
        }

        #region Animation

        private void BornAnimation()
        {
            transform.DOScale(0, startAnimationDuration).SetEase(startAnimationEase).From();
        }

        public void PlayAnimationByTrigger(AnimationType animationType)
        {
            _animationBase.PlayAnimationByTrigger(animationType);
        }

        #endregion

        public void Damage(float damage)
        {
            OnDamage(damage);
        }
        public void Damage(float damage, Vector3 dir)
        {
            transform.DOMove(transform.position - dir, 0.1f);
            OnDamage(damage);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Player p = collision.transform.GetComponent<Player>();

            if(p != null)
            {
                p.healthBase.Damage(1);
            }
        }

        public virtual void Update()
        {
            if(lookAtPlayer && _player != null)
            {
                transform.LookAt(_player.transform.position);
            }
        }
    }
}
