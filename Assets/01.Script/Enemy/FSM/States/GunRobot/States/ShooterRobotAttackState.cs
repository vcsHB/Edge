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
        private int _bulletCnt;
        private bool _isShooting = false;
        
        public ShooterRobotAttackState(Enemy enemy, AnimParamSO anim) : base(enemy, anim)
        {
            _enemy = enemy as ShooterRobot;
        }

        public override void Enter()
        {
            base.Enter();
            if(!_isShooting)
            {
                _bulletCnt = _enemy.bulletCnt;
                _isShooting = true;
                _renderer.CanRotation = false;
            }
            
            _enemy.CanMove = false;
            //_playerPos = _enemy.PlayerManager.PlayerTrm.position;
            _playerPos = _enemy.PlayerManager.PlayerTrm.position;
        }

        public override void Attack()
        {
            base.Attack();
            Bullet bullet1 = PoolManager.Instance.Pop(PoolingType.EnemyBullet) as Bullet;
            Bullet bullet2 = PoolManager.Instance.Pop(PoolingType.EnemyBullet) as Bullet;
            bullet1.transform.position = _enemy.bulletFirePos[0].position;
            bullet2.transform.position = _enemy.bulletFirePos[1].position;
            bullet1.transform.up = _renderer.transform.up;
            bullet2.transform.up = _renderer.transform.up;
        }

        public override void Update()
        {
            base.Update();
            if(_isAnimationEnd)
            {
                _bulletCnt--;
                _enemy.ChangeState(EnemyStateEnum.Attack);
            }

            if (_bulletCnt <= 0)
            {
                _renderer.CanRotation = true;
                _enemy.ChangeState(EnemyStateEnum.Move);
                _isShooting = false;
            }
        }

        public override void Exit()
        {
            base.Exit();
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


