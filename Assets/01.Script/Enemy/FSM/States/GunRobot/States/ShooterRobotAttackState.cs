using Combat;
using ObjectPooling;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Enemys
{
    public class ShooterRobotAttackState : EnemyState
    {
        private ShooterRobot _enemy;
        private Vector3 _playerPos;
        private EnemyRenderer _renderer;
        
        public ShooterRobotAttackState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as ShooterRobot;
            _renderer = enemy.GetCompo<EnemyRenderer>();
        }

        public override void Enter()
        {
            base.Enter();
            _enemy.CanMove = false;
            //_playerPos = _enemy.PlayerManager.PlayerTrm.position;
            _playerPos = _renderer.obj.transform.position;
        }

        public override void Attack()
        {
            base.Attack();
            Bullet bullet1 = PoolManager.Instance.Pop(PoolingType.EnemyBullet) as Bullet;
            Bullet bullet2 = PoolManager.Instance.Pop(PoolingType.EnemyBullet) as Bullet;
            bullet1.transform.position = _enemy.bulletFirePos[0].position;
            bullet2.transform.position = _enemy.bulletFirePos[1].position;
            
            //yield return new WaitForSeconds(_enemy.shootingDelay);
        }

        //private IEnumerator BulletSpawn()
        //{
        //    Vector2 dir = (_playerPos - _enemy.transform.position).normalized;
        //    //나중에 되면 풀링
        //    for (int i = 0; i < _enemy.bulletCnt; ++i)
        //    {
                
        //    }
        //}




    }
}


