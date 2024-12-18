using System;
using Combat;
using UnityEngine;

namespace Agents.Players
{

    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        private Player _player;

        [SerializeField] private Transform _attackHandle;
        private Caster _caster;
        private DamageCaster _damageCaster;


        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _player = agent as Player;

            _player.PlayerInput.OnAttackEvent += HandleAttackEvent;

            _caster = _attackHandle.GetComponent<Caster>();
            _damageCaster = _attackHandle.GetComponent<DamageCaster>();
        }

        private void HandleAttackEvent()
        {
            float damage = _player.PlayerStatus.attackDamage.GetValue();
            _damageCaster.SetDmaage(damage);
            _caster.Cast();

        }
    }

}