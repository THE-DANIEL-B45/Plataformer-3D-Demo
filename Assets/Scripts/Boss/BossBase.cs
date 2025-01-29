using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ebac.StateMachine;
using NaughtyAttributes;
using DG.Tweening;
using System;

namespace Boss
{
    public enum BossAction
    {
        INIT,
        IDLE,
        WALK,
        ATTACK,
        DEATH
    }

    public class BossBase : MonoBehaviour
    {
        [Header("Animation")]
        public float startAnimationduration = 0.5f;
        public Ease startAnimationEase = Ease.OutBack;

        [Header("Attack")]
        public int attackAmount = 5;
        public float timeBetweenAttacks = 0.5f;

        public float speed = 5f;
        public List<Transform> wayPoints;

        public HealthBase healthBase;

        public StateMachine<BossAction> stateMachine;

        private void OnValidate()
        {
            if (healthBase != null) healthBase = GetComponent<HealthBase>();
        }

        private void Awake()
        {
            Init();

            OnValidate();

            healthBase.OnKill += OnBossKill;
        }

        private void Init()
        {
            stateMachine = new StateMachine<BossAction>();
            stateMachine.Init();

            stateMachine.RegisterStates(BossAction.INIT, new BossStateInit());
            stateMachine.RegisterStates(BossAction.WALK, new BossStateWalk());
            stateMachine.RegisterStates(BossAction.ATTACK, new BossStateAttack());
            stateMachine.RegisterStates(BossAction.DEATH, new BossStateDeath());

            SwitchState(BossAction.INIT);
        }

        private void OnBossKill(HealthBase h)
        {
            SwitchState(BossAction.DEATH);
        }

        #region Attack

        public void StartAttack(Action endCallback = null)
        {
            StartCoroutine(AttackCoroutine(endCallback));
        }

        IEnumerator AttackCoroutine(Action endCallback = null)
        {
            int attacks = 0;
            while(attacks < attackAmount)
            {
                attacks++;
                transform.DOScale(1.1f, .2f).SetLoops(2, LoopType.Yoyo);
                yield return new WaitForSeconds(timeBetweenAttacks);
            }
            endCallback?.Invoke();
        }


        #endregion

        #region Walk

        public void GoToRandomPoint(Action onArrive = null)
        {
            int rand = UnityEngine.Random.Range(0, wayPoints.Count);
            StartCoroutine(GoToPointCoroutine(wayPoints[rand], onArrive));
            transform.LookAt(wayPoints[rand]);
        }

        IEnumerator GoToPointCoroutine(Transform t, Action onArrive = null)
        {
            while(Vector3.Distance(transform.position, t.position) > 1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, t.position, Time.deltaTime * speed);
                yield return new WaitForEndOfFrame();
            }
            onArrive?.Invoke();
        }

        #endregion

        #region Animation

        public void StartInitAnimation()
        {
            transform.DOScale(0, startAnimationduration).SetEase(startAnimationEase).From();
            SwitchState(BossAction.WALK);
        }

        #endregion

        #region Debug
        [Button]
        private void SwitchInit()
        {
            SwitchState(BossAction.INIT);
        }

        [Button]
        private void SwitchWalk()
        {
            SwitchState(BossAction.WALK);
        }

        [Button]
        private void SwitchAttack()
        {
            SwitchState(BossAction.ATTACK);
        }

        #endregion

        #region StateMachine

        public void SwitchState(BossAction state)
        {
            stateMachine.SwitchState(state, this);
        }

        #endregion
    }
}
