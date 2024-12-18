using Agents;
using UnityEngine;

namespace Enemys
{
    public class EnemyMover : MonoBehaviour, IAgentComponent
    {
        public Vector2 Movement { get; private set; }
        private Rigidbody2D _rbCompo;
        private Enemy _enemy;

        private void Awake()
        {
            _rbCompo = GetComponent<Rigidbody2D>();
        }

        public void AfterInit()
        {
        }

        public void Dispose()
        {
        }

        public void Initialize(Agent agent)
        {
            _enemy = agent as Enemy;
        }


        void Update()
        {
            if (_enemy.CanMove)
                _rbCompo.linearVelocity = Movement;
        }

        public void SetMovemenet(Vector2 movement)
        {
            Movement = movement;
        }
    }
}

