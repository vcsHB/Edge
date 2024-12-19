using UnityEngine;

namespace Enemys
{
    public class TankAttackState : EnemyState
    {
        private TankEnemy _enemy;
        public TankAttackState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as TankEnemy;
        }

        public override void Update()
        {
            base.Update();
            if(_isAnimationEnd)
            {
                _enemy.ChangeState(EnemyStateEnum.Move);
            }
        }

        public override void Attack()
        {
            base.Attack();
            _enemy.Caster.Cast();
        }
    }
}

