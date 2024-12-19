using Unity.VisualScripting;
using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerNoLimitState : PlayerState
    {
        protected PlayerLimiter _limiter;
        public PlayerNoLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
            _limiter = _player.GetCompo<PlayerLimiter>();
        }



    }
}