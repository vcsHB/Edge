using System;
using Agents.Players;
using Core;
using StatSystem;
using UnityEngine;
using UnityEngine.Jobs;
using WaveSystem;
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
        [Header("NoLimit Setting")]

        [SerializeField] private int _maxFeverScore;

        [SerializeField] private int _currentFeverFill;
        private float _currentNoLimitedTime = 0f;
        private float _noLimitDuration;

        public bool IsNoLimit { get; private set; }

        [Header("Level Up Setting")]
        [SerializeField] private int _levelUpWaveTerm = 4; // 몇 웨이브마다 증강체가 나오는가
        private int _currentWaveStack = 0;

        private void Start()
        {

            _scoreBonusStat = _player.PlayerStatus.scoreBonus;
            _feverFillMultuipleStat = _player.PlayerStatus.feverFillMultiple;
        }

        private void Update()
        {
            if (!IsNoLimit) return;
            _currentNoLimitedTime += Time.deltaTime;
            OnFeverChangedEvent?.Invoke(100 - (int)(_currentNoLimitedTime / _noLimitDuration * 100), 100);
        }


        public void GainScore(int score)
        {
            int addValue = score + (int)(score * 0.1f * _scoreBonusStat.GetValue());
            _score += addValue;
            if (!IsNoLimit)
            {
                int addFeverValue = score + (int)(score * 0.1f * _feverFillMultuipleStat.GetValue());
                _currentFeverFill += addFeverValue;
                OnFeverChangedEvent?.Invoke(_currentFeverFill, _maxFeverScore);
            }
            OnScoreChangedEvent?.Invoke(_score);
            if (_currentFeverFill >= _maxFeverScore)
            {
                _currentFeverFill = 0;
                // 실질적인 노리미트 타임을 적용해야함
                StartNoLimit();
            }
        }


        private void StartNoLimit()
        {
            IsNoLimit = true;
            _noLimitDuration = _player.PlayerStatus.noLimitDuration.GetValue();
            _player.StateMachine.ChangeState("NoLimitEnter");
            _currentNoLimitedTime = 0;

            VolumeManager.Instance.SetChromatic(0.18f);


        }

        public void SetEndNoLimit()
        {
            IsNoLimit = false;
            VolumeManager.Instance.HandleChromaticDisable();
        }


        public void HandleWaveClear()
        {
            _currentWaveStack++;
            if(_currentWaveStack > _levelUpWaveTerm)
            {
                _currentWaveStack = 0;

                _player.HealthCompo.SetMaxHealth();
                UpgradeManager.Instance.DropUpgradeItem();
            }
        } 
    }
}