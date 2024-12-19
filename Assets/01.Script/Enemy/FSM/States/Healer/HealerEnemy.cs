using NUnit;
using UnityEngine;

namespace Enemys
{
    public class HealerEnemy : Enemy
    {
        [Header("Move Setting")]
        public float coolTime;
        public float skillRadius;
        public float safetyDistance;
        public LayerMask targetLayer;
        [Space]
        [Header("HealingSetting")]
        public float healingCnt;
        public float healingAmount;
        public float healingTime;


        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, skillRadius);
        }
    }
}


