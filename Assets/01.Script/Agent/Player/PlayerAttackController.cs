using System;
using Combat;
using UnityEngine;

namespace Agents.Players
{

    public class PlayerAttackController : MonoBehaviour, IAgentComponent
    {
        private Player _player;

        [SerializeField] private PlayerWeapon _weapon;
        [SerializeField] private float _weaponDistance = 1.3f;
        

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

            
        }

        private void HandleAttackEvent()
        {
            float damage = _player.PlayerStatus.attackDamage.GetValue();
            Vector2 mousePos = _player.PlayerInput.MousePosition;
            Vector2 direction = mousePos - (Vector2)transform.position;
            direction.Normalize();
            _weapon.transform.localPosition = direction * _weaponDistance;
           _weapon.Attack(damage, 10f);

        }
    }

}