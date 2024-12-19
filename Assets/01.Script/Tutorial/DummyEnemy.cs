using Combat;
using Managers;
using ObjectPooling;
using StatSystem;
using UnityEngine;

namespace Enemys
{
    public class DummyEnemy : Enemy
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

        public void DummyHit()
        {
            ScoreManager.Instance.GainScore(30);
        }

        public override void Dead()
        {
            base.Dead();
            attackObj.gameObject.SetActive(false);
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

