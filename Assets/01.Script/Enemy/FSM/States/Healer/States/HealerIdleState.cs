using UnityEngine;

namespace Enemys
{
    public class HealerIdleState : EnemyState
    {
        private HealerEnemy _enemy;
        public HealerIdleState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as HealerEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.ActionDelay(2,()=> _enemy.ChangeState(EnemyStateEnum.Move));
        }
    }
}

