using System;
using UnityEngine;

namespace Enemys
{
    
    public class TankMoveState : EnemyState
    {
        private TankEnemy _enemy;
        public TankMoveState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as TankEnemy;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.CanMove = true;
        }

      

        public override void Exit()
        {
            _enemy.CanMove = false;
            base.Exit();
        }

        private void HandleAttackCheck()
        {
            _enemy.ChangeState(EnemyStateEnum.Attack);
        }

        public override void Update()
        {
            base.Update();

            if ((_enemy.PlayerManager.PlayerTrm.position - _enemy.transform.position).sqrMagnitude <= _enemy.radius / 2)
                _enemy.CanMove = false;
            else
                _enemy.CanMove = true;

            Collider2D collider = Physics2D.OverlapCircle(_enemy.transform.position, _enemy.radius, _enemy.targetMask);

            if(collider != null)
            {
                _enemy.ChangeState(EnemyStateEnum.Attack);
            }

        }
    }
}


