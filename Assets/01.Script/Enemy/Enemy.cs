using Agents;
using Players;
using UnityEngine;

namespace Enemys
{
    public class Enemy : Agent
    {
        public PlayerManagerSO Player;
        [SerializeField] private EnemyStateListSO states;

        public bool CanMove { get; set; }
        public EnemyStateMachine StateMachine { get; private set; }

        protected override void Awake()
        {
            StateMachine = new EnemyStateMachine(this, states);
            StateMachine.Initialize(EnemyStateEnum.Idle);
        }

        private void Update()
        {
            StateMachine.Update();
        }


    }
}

