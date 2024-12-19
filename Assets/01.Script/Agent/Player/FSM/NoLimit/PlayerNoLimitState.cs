using Unity.VisualScripting;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerNoLimitState : PlayerState
    {
        private PlayerLimiter _limiter;
        public PlayerNoLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
            _limiter = _player.GetCompo<PlayerLimiter>();
        }
        public override void Enter()
        {
            base.Enter();
        }

        public override void UpdateState()
        {
            base.UpdateState();
           
        }


        public override void Exit()
        {
            base.Exit();
           

        }


    }
}