using UnityEngine;
using UnityEngine.UI;

namespace Enemys
{
    public class EnemyState
    {
        protected bool _isAnimationEnd;
        private Enemy _enemy;
        protected AnimParamSO _animParam;
        protected EnemyRenderer _renderer;
        protected EnemyAnimationTrigger _animTrigger;
        public EnemyState(Enemy enemy,AnimParamSO anim)
        {
            _enemy = enemy;
            _animParam = anim;
            _renderer = enemy.GetCompo<EnemyRenderer>();
            _animTrigger = enemy.GetCompo<EnemyAnimationTrigger>(); 
        }

        public virtual void Enter()
        {
            _renderer.SetParam(_animParam, true);
            _animTrigger.AttackEvent += Attack;

            _isAnimationEnd = false;
        }

        public virtual void Exit()
        {
            _animTrigger.AttackEvent -= Attack;
            _renderer.SetParam(_animParam, false);

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

