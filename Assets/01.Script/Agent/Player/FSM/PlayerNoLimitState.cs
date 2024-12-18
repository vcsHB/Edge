using UnityEngine;
namespace Agents.Players.FSM
{
    public class PlayerNoLimitState : PlayerState
    {

        public PlayerNoLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }


       public override void UpdateState()
        {
            base.UpdateState();
            Vector2 direction =_player.PlayerInput.InputDirection;
            _mover.SetMovement(direction);
            if(Mathf.Approximately(direction.x, 0))
            {
                _stateMachine.ChangeState("Idle");
            }
        }


    }
}