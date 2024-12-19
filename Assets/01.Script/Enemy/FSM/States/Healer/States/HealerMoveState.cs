using UnityEngine;

namespace Enemys
{
    public class HealerMoveState : EnemyState
    {
        private HealerEnemy _enemy;
        private float _listTime = 0;
        public HealerMoveState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as HealerEnemy;
        }

        public override void Update()
        {
            base.Update();
            if (Vector2.Distance(_enemy.PlayerManager.PlayerTrm.position, _enemy.transform.position) > _enemy.safetyDistance)
            {
                _enemy.CanMove = true;
            }
            else
                _enemy.CanMove = false;

            if (_listTime + _enemy.coolTime < Time.time && !_enemy.CanMove)
                _enemy.ChangeState(EnemyStateEnum.Skill);

        }

        public override void Exit()
        {
            _listTime = Time.time;
            _enemy.CanMove = false;
            base.Exit();
        }
    }
}

