using UnityEngine;
namespace Agents.Players
{


    public class PlayerLimiter : MonoBehaviour, IAgentComponent
    {

        private float _currentTime = 0f;
        private float _noLimitDuration;
        private Player _player;

        public void Initialize(Agent agent)
        {
            _player = agent as Player;

        }
        public void AfterInit()
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }


        public void SetUnLimit()
        {
            _player.PlayerStatus.defense.AddModifier(200f);
            _player.PlayerStatus.attackDamage.AddModifier(200f);
            _player.PlayerStatus.attackSpeed.AddModifier(2f);
            _noLimitDuration = _player.PlayerStatus.noLimitDuration.GetValue();
            _currentTime = 0f;

        }

        public void SetLimit()
        {
            _player.PlayerStatus.defense.RemoveModifier(200f);
            _player.PlayerStatus.attackDamage.RemoveModifier(200f);

            _player.PlayerStatus.attackSpeed.RemoveModifier(2f);
        }

        private void Update()
        {
            _currentTime += Time.deltaTime;

            if (_currentTime > _noLimitDuration)
            {
                _currentTime = 0f;
                
                _player.StateMachine.ChangeState("Limit");
            }
        }


    }
}