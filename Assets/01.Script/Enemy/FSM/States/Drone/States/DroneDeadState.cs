using UnityEngine;

namespace Enemys
{
    public class DroneDeadState : EnemyState
    {
        private Enemy _enemy;
        public DroneDeadState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy;
        }

        public override void Enter()
        {
            base.Enter();
            //나중에 불링?
            GameObject.Destroy(_enemy.gameObject);
        }
    }
}

