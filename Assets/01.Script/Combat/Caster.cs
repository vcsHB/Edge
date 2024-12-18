using UnityEngine;
using UnityEngine.Events;
namespace Combat
{

    public class Caster : MonoBehaviour
    {
        public UnityEvent OnCastEvent;
        [Header("Caster Settings")]
        [SerializeField] private Vector2 _offset;
        [SerializeField] private LayerMask _targetLayer;
        [SerializeField] private float _castRadius = 1f;
        [SerializeField] private int _castTargetMaxAmount = 1;
        

        private ICastable[] _casters;
        private Collider2D[] _hits;

        private void Awake()
        {
            _casters = GetComponentsInChildren<ICastable>();

        }

        public void Cast()
        {
            _hits = Physics2D.OverlapCircleAll((Vector2)transform.position + _offset, _castRadius, _targetLayer);
            for (int i = 0; i < _hits.Length; i++)
            {
                for (int j = 0; j < _casters.Length; j++)
                {
                    _casters[j].Cast(_hits[i]);
                }
            }
            OnCastEvent?.Invoke();
        }

    }
}