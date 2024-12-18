using Combat;
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

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, explosionRange);
        }
    }
}

