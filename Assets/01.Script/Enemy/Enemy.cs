using Agents;
using Players;
using UnityEngine;

namespace Enemys
{
    public class Enemy : Agent
    {
        public PlayerManagerSO PlayerManager;
        [SerializeField] private EnemyStateListSO states;

        public bool CanMove { get; set; } = true;
        public EnemyStateMachine StateMachine { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            StateMachine = new EnemyStateMachine(this, states);
            StateMachine.Initialize(EnemyStateEnum.Idle);
        }

        private void Update()
        {
            StateMachine.Update();
        }
        
        public void ChangeState(EnemyStateEnum changeState)
        {
            StateMachine.ChangeState(changeState);
        }


    }
}

