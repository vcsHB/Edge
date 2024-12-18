using Combat;
using ObjectPooling;
using StatSystem;
using UnityEngine;

namespace Enemys
{
    public class DroneEnemy : Enemy, IPoolable
    {
        public float explosionRange;
        public LayerMask targetLayer;
        public DamageCaster DamageCaster { get; private set; }
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        public Transform attackObj;

        protected override void Awake()
        {
            base.Awake();
            DamageCaster = GetComponentInChildren<DamageCaster>();

        }
        public void ResetItem()
        {

        }
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, explosionRange);
        }


#endif
    }
}

