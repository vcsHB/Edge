using ObjectPooling;
using UnityEngine;

namespace Enemys
{
    public class ShooterRobot : Enemy, IPoolable
    {
        public int bulletCnt;
        public float moveTime;
        public Transform [] bulletFirePos;

        [field:SerializeField] public PoolingType type { get ; set ; }

        public GameObject ObjectPrefab => gameObject;

        public void ResetItem()
        {
            throw new System.NotImplementedException();
        }
    }
}


