using UnityEngine;

namespace Enemys
{
    public class DroneMoveState : EnemyState
    {
        DroneEnemy _drone;
        public DroneMoveState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _drone = enemy as DroneEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _drone.CanMove = true;
        }

        public override void Exit()
        {
            _drone.CanMove = false;
            base.Exit();
        }

        public override void Update()
        {
            base.Update();
            Collider2D target = Physics2D.OverlapCircle(_drone.transform.position
                , _drone.explosionRange-0.5f, _drone.targetLayer);
            if (target != null)
            {
                _drone.ChangeState(EnemyStateEnum.Attack);
            }
        }
    }
}


