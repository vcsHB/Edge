using UnityEngine;

namespace Enemys
{
    public class DroneDeadState : EnemyState
    {
        public DroneDeadState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
        }

        public override void Enter()
        {
            base.Enter();
            //나중에 불링?
            GameObject.Destroy(_enemy);
        }
    }
}

