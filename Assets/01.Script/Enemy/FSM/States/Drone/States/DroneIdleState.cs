using UnityEngine;

namespace Enemys
{
    public class DroneIdleState : EnemyState
    {
        public DroneIdleState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
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

