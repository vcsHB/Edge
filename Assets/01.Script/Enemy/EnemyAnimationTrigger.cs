using Agents;
using Enemys;
using System;
using UnityEngine;

namespace Enemys
{
    public class EnemyAnimationTrigger : MonoBehaviour, IAgentComponent
    {
        private Enemy _enemy;
        private Animator _animator;
        public event Action AttackEvent;
        public void AfterInit()
        {

        }

        public void Dispose()
        {

        }

        public void Initialize(Agent agent)
        {
            _enemy = agent as Enemy;
            _animator = GetComponent<Animator>();
        }

        public void AnimationEnd()
        {
            _enemy.StateMachine.CurrentState.AnimationEndTrigger();
        }

        public void AttackTrigger()
        {
            AttackEvent?.Invoke();
            _enemy.StateMachine.CurrentState.Attack();
        }

    }
}

