using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemys
{
    public class ShooterRobotAttackState : EnemyState
    {
        ShooterRobot _enemy;

        
        public ShooterRobotAttackState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as ShooterRobot;
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.CanMove = false;
        }

        public override void Attack()
        {
            base.Attack();
            //나중에 되면 풀링
            for(int i =0;i<_enemy.bulletCnt;++i)
            {
                
            }
        }




    }
}


