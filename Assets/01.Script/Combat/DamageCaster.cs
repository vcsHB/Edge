using UnityEngine;

namespace Combat
{

    public class DamageCaster : MonoBehaviour, ICastable
    {
        [SerializeField] private float _damage;

        public void Cast(Collider2D target)
        {
            if(target.TryGetComponent(out IDamageable damageable))
            {
                damageable.ApplyDamage(_damage);
            }
        }


    }

}