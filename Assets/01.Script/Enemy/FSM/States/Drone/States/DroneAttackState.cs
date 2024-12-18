using Agents;
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
            _enemy.CanMove = false;
            Debug.Log(1);
        }

        public override void Update()
        {
            base.Update();
            if(_isAnimationEnd)
            {
                if(_enemy.PlayerManager.Player.TryGetComponent<Health>(out Health health))
                {
                    health.ApplyDamage(_drone.damage);
                }
            }
        }

        public override void Exit()
        {
            
            base.Exit();
        }
    }
}

