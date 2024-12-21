using Combat;
using UnityEngine;

namespace Enemys
{

    public class TankEnemy : Enemy
    {
        public Caster Caster { set; get; }
        public float radius;
        public LayerMask targetMask;
        public float attackCoolTime;

        protected override void Awake()
        {
            base.Awake();
            Caster = GetComponentInChildren<Caster>();
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position,radius);
        }
    }
}

