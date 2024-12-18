using Combat;
using ObjectPooling;
using StatSystem;
using UnityEngine;

namespace Enemys
{
    public class DroneEnemy : Enemy
    {
        public float explosionRange;
        public LayerMask targetLayer;
        public DamageCaster DamageCaster { get; private set; }


        public Transform attackObj;

        protected override void Awake()
        {
            base.Awake();
            DamageCaster = GetComponentInChildren<DamageCaster>();

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

