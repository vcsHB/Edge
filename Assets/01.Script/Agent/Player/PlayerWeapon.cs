using Combat;
using UnityEngine;
namespace Agents.Players
{

    public class PlayerWeapon : MonoBehaviour
    {
        private Caster _caster;
        private DamageCaster _damageCaster;
        [SerializeField] private ParticleSystem _attackVFX;

        public void Attack(float damage, float knockbackPower)
        {
            _damageCaster.SetDamage(damage);
            _caster.Cast();
            _attackVFX.Play();
        }


        private void Awake()
        {
            _caster = GetComponent<Caster>();
            _damageCaster = GetComponent<DamageCaster>();

        }
    }
}