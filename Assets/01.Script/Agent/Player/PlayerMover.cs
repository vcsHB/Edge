using System;
using ObjectManage;
using UnityEngine;
namespace Agents.Players
{
    public class PlayerMover : MonoBehaviour, IAgentComponent
    {
        public Action<Vector2> OnMovement;
        private Player _player;
        [SerializeField] private LayerMask _moveTargetLayer;
        [SerializeField] private float _moveTargetDetectLength = 50f;
        private Rigidbody2D _rigidCompo;
        public bool isEdgeMove;
        private Vector2 _moveDirection;
        public Vector2 Velocity { get; private set; }
        public void Initialize(Agent agent)
        {
            _player = agent as Player;
            _rigidCompo = GetComponent<Rigidbody2D>();
        }

        public void AfterInit()
        {

        }

        public void Dispose()
        {
        }


        public void MoveToTarget(Vector2 direction)
        {
            if(!isEdgeMove) return;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _moveTargetDetectLength, _moveTargetLayer);

            if (hit.collider == null)
                return;

            if (hit.collider.TryGetComponent(out MovePoint movePoint))
            {


            }

        }



        private void FixedUpdate()
        {
            if (isEdgeMove)
            {
                Velocity = _moveDirection * _player.PlayerStatus.moveSpeed.GetValue();
                _rigidCompo.linearVelocity = Velocity;

                OnMovement?.Invoke(Velocity);
                Velocity = _rigidCompo.linearVelocity;
            }
            else
            {

            }


        }


        public void SetMovement(Vector2 direction)
        {
            _moveDirection = direction;

        }
    }
}