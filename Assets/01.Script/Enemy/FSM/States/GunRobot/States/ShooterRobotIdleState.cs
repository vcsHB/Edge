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
            _enemy.StartCoroutine(StartDelay());
        }

        public override void Update()
        {
            base.Update();
            
        }

        private IEnumerator StartDelay()
        {
            yield return new WaitForSeconds(1);
            _enemy.ChangeState(EnemyStateEnum.Move);
        }
    }


}

