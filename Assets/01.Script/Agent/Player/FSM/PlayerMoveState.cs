using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerMoveState : PlayerState
    {
        

        public PlayerMoveState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
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