using Unity.VisualScripting;
using UnityEngine;

namespace Enemys
{
    public class ShooterRobot : Enemy
    {
        [SerializeField] private GameObject _bulletPrefab;
        public int bulletCnt;
        public float shootingCoolTime;
        public float moveTime;

        
        
        private void SpawnBullet()
        {
            for(int i =0;i<bulletCnt;++i)
            {
                 Instantiate(_bulletPrefab);
            }
        }

    }
}


