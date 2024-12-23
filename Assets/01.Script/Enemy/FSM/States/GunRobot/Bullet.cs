using ObjectManage;
using ObjectPooling;
using UnityEngine;

namespace Combat
{
    public class Bullet : MonoBehaviour, IPoolable, IDamageable
    {
        private Rigidbody2D _rbCompo;
        public Vector2 MoveDir { get; set; }
        [field: SerializeField] public PoolingType type { get; set; }

        public GameObject ObjectPrefab => gameObject;

        [SerializeField] private float _speed;
        [SerializeField] private PoolingType _breakVFXType;
        private Caster _caster;


        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
            _caster = GetComponentInChildren<Caster>();
            _caster.OnCastEvent.AddListener(HandleDestroyEvent);
        }

        private void OnDestroy()
        {
        }

        private void Update()
        {
            _rbCompo.linearVelocity = transform.up * _speed;
            _caster.Cast();
        }

        public void ResetItem()
        {
        }

        private void HandleDestroyEvent()
        {
            VFXPlayer vfx = PoolManager.Instance.Pop(_breakVFXType) as VFXPlayer;
            vfx.transform.position = transform.position;
            vfx.Play();
            PoolManager.Instance.Push(this);
        }

        public void ApplyDamage(float damage)
        {
            HandleDestroyEvent();
        }
    }

}
