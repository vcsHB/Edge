using UnityEngine;

namespace Enemys
{
    public class DroneEnemy : Enemy
    {
        public float explosionRange;
        public LayerMask targetLayer;
        public int damage;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, explosionRange);
        }
    }
}

