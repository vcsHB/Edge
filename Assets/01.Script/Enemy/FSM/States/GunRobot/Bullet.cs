using Agents;
using Agents.Players;
using Combat;
using ObjectPooling;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour, IPoolable
    {
        private Rigidbody2D _rbCompo;
        public Vector2 MoveDir { get; set; }
        [field:SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private float _speed;
        private Caster _caster;


        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
            _caster = GetComponentInChildren<Caster>();
        }

        private void Update()
        {
            _rbCompo.linearVelocity = transform.up * _speed;
            _caster.Cast();
        }

        public void ResetItem()
        {
        }
    }

}
