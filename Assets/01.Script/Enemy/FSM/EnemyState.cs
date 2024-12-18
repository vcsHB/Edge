using UnityEngine;

namespace Enemys
{
    public class EnemyState
    {
        protected bool _isAnimationEnd;
        protected Enemy _enemy;
        protected AnimParamSO _animParam;
        protected EnemyRenderer _animator;
        public EnemyState(Enemy enemy,AnimParamSO anim)
        {
            _enemy = enemy;
            _animParam = anim;
            _animator = enemy.GetCompo<EnemyRenderer>();
        }

        public virtual void Enter()
        {
            _animator.SetParam(_animParam, true);
            _isAnimationEnd = false;
        }

        public virtual void Exit()
        {
            _animator.SetParam(_animParam, false);

        }
        public virtual void Update()
        {

        }

        public virtual void AnimationEndTrigger()
        {
            _isAnimationEnd = true;
        }

        public virtual void Attack()
        {

        }
    }
}

