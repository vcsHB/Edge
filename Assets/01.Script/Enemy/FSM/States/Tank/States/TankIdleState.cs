using UnityEngine;

namespace Enemys
{
    public class TankIdleState : EnemyState
    {
        private TankEnemy _enemy;
        public TankIdleState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as TankEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.ActionDelay(2,()=> _enemy.ChangeState(EnemyStateEnum.Move));
        }
    }
}

