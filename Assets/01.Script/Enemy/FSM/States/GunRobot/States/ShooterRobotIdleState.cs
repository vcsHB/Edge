using UnityEngine;
using DG.Tweening;
using System.Collections;

namespace Enemys
{
    public class ShooterRobotIdleState : EnemyState
    {
        private ShooterRobot _enemy;
        public ShooterRobotIdleState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as ShooterRobot;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.ActionDelay(1, () => _enemy.ChangeState(EnemyStateEnum.Move));
        }

        public override void Update()
        {
            base.Update();
            
        }
    }


}

