using System;
using Agents.Players;
using Core;
using StatSystem;
using UnityEngine;
namespace Managers
{

    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        public event Action<int> OnScoreChangedEvent;
        public event Action<int, int> OnFeverChangedEvent;
        [SerializeField] private Player _player;

        [SerializeField] private int _score;
        public int Score => _score;

        private Stat _scoreBonusStat;
        private Stat _feverFillMultuipleStat;
        [SerializeField] private int _maxFeverScore;

        [SerializeField] private int _currentFeverFill;


        private void Start()
        {

            _scoreBonusStat = _player.PlayerStatus.scoreBonus;
            _feverFillMultuipleStat = _player.PlayerStatus.feverFillMultiple;
        }


        public void GainScore(int score)
        {
            int addValue = score + (int)(score * 0.1f * _scoreBonusStat.GetValue());
            int addFeverValue = score + (int)(score * 0.1f * _feverFillMultuipleStat.GetValue());
            _score += addValue;
            _currentFeverFill += addFeverValue; 
            OnScoreChangedEvent?.Invoke(_score);
            if(_currentFeverFill >= _maxFeverScore)
            {
                _currentFeverFill = 0;
                // 실질적인 노리미트 타임을 적용해야함
                PlayerManager.Instance.Player.StateMachine.ChangeState("NoLimitIdle");
            }
            OnFeverChangedEvent?.Invoke(_currentFeverFill, _maxFeverScore);
        }
    }
}