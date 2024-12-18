using System;
using ObjectManage;
using UnityEngine;
using UnityEngine.Events;
namespace Agents.Players
{
    public class PlayerMover : MonoBehaviour, IAgentComponent
    {

        [Header("Events")]
        public UnityEvent OnArriveEvent;
        [Header("Essential Setting")]
        public Action<Vector2> OnMovement;
        private Player _player;
        [SerializeField] private LayerMask _moveTargetLayer;
        [SerializeField] private float _moveTargetDetectLength = 50f;
        private Rigidbody2D _rigidCompo;

        // Move
        public bool isEdgeMove;
        private Vector2 _previousPosition;
        private MovePoint _targetPoint;
        public float MoveDistance => Vector2.Distance(_previousPosition, _targetPoint.transform.position);

        private Vector2 _moveDirection;
        public Vector2 Velocity { get; private set; }
        private Collider2D[] hits;
        private float _limitDetectRadius = 100f;

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


        public bool SetMoveTarget(Vector2 direction)
        {
            if (!isEdgeMove) return false;
            SetPreviousPos(transform.position);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, _moveTargetDetectLength, _moveTargetLayer);

            if (hit.collider == null)
                return false;
                Debug.Log("쏨");

            if (hit.collider.TryGetComponent(out MovePoint movePoint))
            {
                SetMovePoint(movePoint);
            }

            return true;
        }

        public void SetPreviousPos(Vector2 position)
        {
            _previousPosition = position;
        }

        private void FixedUpdate()
        {
            if (!isEdgeMove)
            {
                Velocity = _moveDirection * _player.PlayerStatus.moveSpeed.GetValue();
                _rigidCompo.linearVelocity = Velocity;

                OnMovement?.Invoke(Velocity);
            }
        }



        #region Edge move

        public void SetMovement(Vector2 direction)
        {
            _moveDirection = direction;
        }

        public void SetMovement(float ratio)
        {
            Vector2 lerpPos = Vector2.Lerp(_previousPosition, _targetPoint.transform.position, ratio);
            transform.position = lerpPos;
            if (ratio >= 1f)
            {
                OnArriveEvent?.Invoke();
                _targetPoint.Enter();

            }
        }
        #endregion

        public void SetMovePoint(MovePoint newPoint)
        {
            if (_targetPoint != null)
                _targetPoint.Exit();

            _targetPoint = newPoint;
        }

        public MovePoint GetNearMovePoint()
        {
            hits = Physics2D.OverlapCircleAll(transform.position, _limitDetectRadius, _moveTargetLayer);
            if (hits.Length < 1)
            {
                Debug.LogError("Can't Find Near MovePoint");
                return null;
            }
            Collider2D near = hits[0];
            Vector2 playerPos = _player.transform.position;
            for (int i = 1; i < hits.Length; i++)
            {
                if (Vector2.Distance(hits[i].transform.position, playerPos) < Vector2.Distance(near.transform.position, playerPos))
                    near = hits[i];
            }

            return near.GetComponent<MovePoint>();
        }
    }
}