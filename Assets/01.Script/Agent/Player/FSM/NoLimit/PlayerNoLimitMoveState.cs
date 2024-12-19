using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerNoLimitMoveState : PlayerNoLimitState
    {
        public PlayerNoLimitMoveState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        
    }
}