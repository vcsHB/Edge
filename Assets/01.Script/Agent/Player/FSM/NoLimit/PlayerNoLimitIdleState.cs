using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerNoLimitIdleState : PlayerNoLimitState
    {
        public PlayerNoLimitIdleState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }

        public override void Enter()
        {
            base.Enter();
        }


        public override void Exit()
        {
            base.Exit();
        }


    }
}