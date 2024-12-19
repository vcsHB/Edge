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

        public bool IsNoLimit { get; private set; }


        private void Start()
        {

            _scoreBonusStat = _player.PlayerStatus.scoreBonus;
            _feverFillMultuipleStat = _player.PlayerStatus.feverFillMultiple;
        }


        public void GainScore(int score)
        {
            int addValue = score + (int)(score * 0.1f * _scoreBonusStat.GetValue());
            _score += addValue;
            if (!IsNoLimit)
            {
                int addFeverValue = score + (int)(score * 0.1f * _feverFillMultuipleStat.GetValue());
                _currentFeverFill += addFeverValue;
            }
            OnScoreChangedEvent?.Invoke(_score);
            if (_currentFeverFill >= _maxFeverScore)
            {
                _currentFeverFill = 0;
                // 실질적인 노리미트 타임을 적용해야함
                StartNoLimit();
            }
            OnFeverChangedEvent?.Invoke(_currentFeverFill, _maxFeverScore);
        }


        private void StartNoLimit()
        {
            IsNoLimit = true;
            PlayerManager.Instance.Player.StateMachine.ChangeState("NoLimitEnter");
            VolumeManager.Instance.SetChromatic(0.12f);


        }

        public void SetEndNoLimit()
        {
            IsNoLimit = false;
            VolumeManager.Instance.HandleChromaticDisable();
        }
    }
}