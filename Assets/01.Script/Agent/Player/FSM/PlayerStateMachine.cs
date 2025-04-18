using System;
using System.Collections.Generic;
using UnityEngine;
namespace Agents.Players.FSM
{

    public class PlayerStateMachine
    {
        private Dictionary<string, PlayerState> _stateDictionary = new ();
        public PlayerState CurrentState { get; private set; }
        private Player _player;
        public PlayerStateMachine(Player player)
        {
            _player = player;
        }



        public void Initialize(string firstState)
        {
            AddState("Idle");
            AddState("Move");
            AddState("NoLimitEnter");
            AddState("NoLimitIdle");
            AddState("NoLimitMove");
            AddState("Limit");

            if (_stateDictionary.TryGetValue(firstState, out PlayerState state))
            {
                CurrentState = state;
                CurrentState.Enter();
            }
        }

        public void AddState(string name)
        {
            Type t = Type.GetType($"Agents.Players.FSM.Player{name}State");
            PlayerState state = Activator.CreateInstance(t, _player, this, 0) as PlayerState;
            _stateDictionary.Add(name, state);
        }

        public void UpdateState()
        {
            CurrentState.UpdateState();
        }

        public void ChangeState(string name)
        {
            if (_stateDictionary.TryGetValue(name, out PlayerState state))
            {
                CurrentState.Exit();
                CurrentState = state;
                CurrentState.Enter();
            }
        }


    }
}