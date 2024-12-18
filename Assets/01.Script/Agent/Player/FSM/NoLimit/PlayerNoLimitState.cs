using Unity.VisualScripting;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerNoLimitState : PlayerState
    {
        private float _currentTime = 0f;
        private float _noLimitDuration;
        public PlayerNoLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }
        public override void Enter()
        {
            base.Enter();
            _noLimitDuration = _player.PlayerStatus.noLimitDuration.GetValue();
        }

        public override void UpdateState()
        {
            base.UpdateState();
            _currentTime += Time.deltaTime;
            
            if(_currentTime > _noLimitDuration)
            {
                _currentTime = 0f;
                _stateMachine.ChangeState("Limit");
            }
        }


        public override void Exit()
        {
            base.Exit();
            _player.PlayerStatus.defense.RemoveModifier(200f);
            _player.PlayerStatus.attackSpeed.RemoveModifier(2f);

        }


    }
}