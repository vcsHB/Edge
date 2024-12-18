using UnityEngine;

namespace Enemys
{
    public class DroneIdleState : EnemyState
    {
        private Enemy _enemy;
        public DroneIdleState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.CanMove = false;
            _enemy.ChangeState(EnemyStateEnum.Move);
        }

        public override void Exit()
        {
            base.Exit();
        }

        
    }
}

