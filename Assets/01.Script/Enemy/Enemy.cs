using Agents;
using ObjectPooling;
using Players;
using StatSystem;
using System;
using System.Collections;
using UnityEngine;

namespace Enemys
{
    public class Enemy : Agent, IPoolable
    {
        public PlayerManagerSO PlayerManager;
        [SerializeField] private EnemyStateListSO states;
        public Action<Enemy> OnDeadEvent;
        [field:SerializeField]public StatusSO Stat{get;private set;}
        public Health HealthCompo{get; private set;}

        public bool CanMove { get; set; } = true;
        public EnemyStateMachine StateMachine { get; private set; }
        [field:SerializeField]public PoolingType type { get ; set ; }
        public int dropScore;

        public GameObject ObjectPrefab => gameObject;

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine(this, states);
            StateMachine.Initialize(EnemyStateEnum.Idle);
            HealthCompo = GetComponent<Health>();
            HealthCompo.Initialize(Stat.health.GetValue());
        }

        private void Update()
        {
            StateMachine.Update();
        }
        
        public void ChangeState(EnemyStateEnum changeState)
        {
            StateMachine.ChangeState(changeState);
        }

        public void ActionDelay(float delay,Action action,bool firstAndList = false)
        {
            StartCoroutine(Delay(delay, action, firstAndList));
        }

        private IEnumerator Delay(float delay, Action action, bool firstAndList = false)
        {
            if(firstAndList)
            {
                yield return new WaitForSeconds(delay);
                action?.Invoke();
            }
            else
            {
                action?.Invoke();
                yield return new WaitForSeconds(delay);
            }

        }

        public virtual void Dead()
        {
            
            OnDeadEvent?.Invoke(this);
            PoolManager.Instance.Push(this);
        }

        public void UpgradeEnemyStat(int level)
        {
            Stat.health.baseValue += level * 10;
        }

        public void ResetItem()
        {
        }
    }
}

