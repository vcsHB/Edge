using Agents;
using Combat;
using UnityEngine;

namespace Enemys
{
    public class DroneAttackState : EnemyState
    {
        private DroneEnemy _drone;
        public DroneAttackState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _drone = enemy as DroneEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _drone.CanMove = false;
            _drone.attackObj.localScale = Vector3.one * _drone.explosionRange *2;
        }

        public override void Update()
        {
            base.Update();
            if(_isAnimationEnd)
            {
                _drone.ChangeState(EnemyStateEnum.Dead);
            }
        }

        public override void Attack()
        {
            Collider2D target = Physics2D.OverlapCircle(_drone.transform.position
                , _drone.explosionRange - 0.5f, _drone.targetLayer);
            if(target != null)
            {
                _drone.DamageCaster.Cast(target);
            }
        }

        public override void Exit()
        {
            
            base.Exit();
        }
    }
}

