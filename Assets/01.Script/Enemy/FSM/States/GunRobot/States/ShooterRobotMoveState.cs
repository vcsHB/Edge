using UnityEngine;

namespace Enemys
{
    public class ShooterRobotMoveState : EnemyState
    {
        private ShooterRobot _enemy;
        private float _moveTime;
        public ShooterRobotMoveState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as ShooterRobot;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.CanMove = true;
            _moveTime = _enemy.moveTime;
        }

        public override void Update()
        {
            base.Update();
            if (_moveTime >= 0)
            {
                _moveTime -= Time.deltaTime;
            }
            else
                _enemy.ChangeState(EnemyStateEnum.Attack);
        }

        public override void Exit()
        {
            _enemy.CanMove = false;
            base.Exit();
        }


    }
}

