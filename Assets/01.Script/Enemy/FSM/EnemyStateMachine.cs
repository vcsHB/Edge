using Agents;
using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

namespace Enemys
{
    public class EnemyStateMachine
    {

        private Dictionary<EnemyStateEnum,EnemyState> _enemyStates;
        public EnemyState CurrentState { get; private set; }


        public EnemyStateMachine(Enemy agent,EnemyStateListSO stateList)
        {
            _enemyStates = new Dictionary<EnemyStateEnum, EnemyState>();

            foreach (EnemyStateSO state in stateList.states)
            {
                try
                {
                    Type t = Type.GetType(state.className);
                    var entityState = Activator.CreateInstance(t, agent, state.animParam) as EnemyState;
                    _enemyStates.Add(state.stateEnum, entityState);
                }
                catch (Exception ex)
                {
                    Debug.LogError($"{state.className} ¾ø´Ù°í");
                }
            }

        }

        public void Initialize(EnemyStateEnum startState)
        {
            CurrentState = GetState(startState);
            CurrentState.Enter();
        }

        public void ChangeState(EnemyStateEnum changeState)
        {
            CurrentState.Exit();
            CurrentState = GetState(changeState);
            CurrentState.Enter();
        }

        public void Update()
        {
            CurrentState.Update();
        }

        public EnemyState GetState(EnemyStateEnum state) => _enemyStates.GetValueOrDefault(state);
    }
}

