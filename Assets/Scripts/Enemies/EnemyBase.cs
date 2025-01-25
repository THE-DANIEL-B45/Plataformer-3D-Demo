using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Animation;


namespace Enemy
{
    public class EnemyBase : MonoBehaviour, IDamageable
    {
        public Collider myCollider;
        public FlashColor flashColor;
        public ParticleSystem myParticleSystem;
        public float startLife = 10f;

        [SerializeField] private float _currentLife;

        [SerializeField] private AnimationBase _animationBase;

        [Header("Start Animation")]
        public float startAnimationDuration = 0.2f;
        public Ease startAnimationEase = Ease.OutBack;
        public bool startWithBornAnimation = true;

        private void Awake()
        {
            Init();
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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                OnDamage(5f);
            }
        }

        public void Damage(float damage)
        {
            OnDamage(damage);
        }
    }
}
