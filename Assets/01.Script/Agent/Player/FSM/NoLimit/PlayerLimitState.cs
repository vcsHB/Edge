using System.Collections;
using ObjectManage;
using UnityEngine;
namespace Agents.Players.FSM
{


    public class PlayerLimitState : PlayerState
    {
        private MovePoint _targetPoint;
        private float _backDuration;

        public PlayerLimitState(Player player, PlayerStateMachine stateMachine, int animationHash) : base(player, stateMachine, animationHash)
        {
        }


        public override void Enter()
        {
            base.Enter();
            _player.StartCoroutine(LimitCoroutine());
        }

        private IEnumerator LimitCoroutine()
        {
            _targetPoint = _mover.GetNearMovePoint();
            _mover.SetPreviousPos(_player.transform.position);

            float currentTime = 0f;
            while (currentTime <= _backDuration)
            {
                currentTime += Time.deltaTime;
                _mover.SetMovement(currentTime / _backDuration);
                yield return null;
            }
            _stateMachine.ChangeState("Idle");
        }
    }
}