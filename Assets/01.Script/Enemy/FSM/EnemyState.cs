using UnityEngine;

namespace Enemys
{
    public class EnemyState
    {
        private bool _isAnimationEnd;
        protected Enemy _enemy;
        protected AnimParamSO _animParam;
        protected Animator _animator;
        public EnemyState(Enemy enemy,AnimParamSO anim)
        {
            _enemy = enemy;
            _animParam = anim;
            _animator = enemy.GetComponent<Animator>();
        }

        public virtual void Enter()
        {
            _animator.SetBool(_animParam.animHash, true);
            _isAnimationEnd = false;
        }

        public virtual void Exit()
        {
            _animator.SetBool(_animParam.animHash, false);

        }
        public virtual void Update()
        {

        }

        public virtual void AnimationEndTrigger()
        {
            _isAnimationEnd = true;
        }
    }
}

