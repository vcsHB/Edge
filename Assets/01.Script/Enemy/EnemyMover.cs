using Agents;
using UnityEngine;

namespace Enemys
{
    public class EnemyMover : MonoBehaviour, IAgentComponent
    {
        [SerializeField] float _speed;
        public Vector2 Movement { get; private set; }
        private Rigidbody2D _rbCompo;
        private Enemy _enemy;

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _enemy = agent as Enemy;
            _rbCompo = agent.GetComponent<Rigidbody2D>();

        }


        void Update()
        {
            if (_enemy.CanMove)
                _rbCompo.linearVelocity = transform.right * _speed;
            else
                _rbCompo.linearVelocity = Vector2.zero;

        }

        public void SetMovemenet(Vector2 movement)
        {
            Movement = movement;
        }
    }
}

