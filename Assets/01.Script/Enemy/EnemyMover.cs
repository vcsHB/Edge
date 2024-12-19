using Agents;
using Agents.Players;
using UnityEngine;

namespace Enemys
{
    public class EnemyMover : MonoBehaviour, IAgentComponent
    {
        public Vector2 Movement { get; private set; }
        private Rigidbody2D _rbCompo;
        private Enemy _enemy;

        private EnemyRenderer _rend;

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
            _rend = agent.GetCompo<EnemyRenderer>();

        }


        void Update()
        {
            if (_enemy.CanMove)
            {
                Vector2 dir = (_enemy.PlayerManager.PlayerTrm.position - transform.position).normalized;
                //Vector2 dir = (_rend.obj.transform.position - transform.position).normalized;
                _rbCompo.linearVelocity = dir * _enemy.Stat.moveSpeed.baseValue;

            }
            else
                _rbCompo.linearVelocity = Vector2.zero;
        }

        public void SetMovemenet(Vector2 movement)
        {
            Movement = movement;
        }
    }
}

