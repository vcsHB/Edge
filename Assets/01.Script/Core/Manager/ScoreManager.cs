using System;
using UnityEngine;
namespace Managers
{

    public class ScoreManager : MonoSingleton<ScoreManager>
    {
        public event Action<int> OnScoreChangedEvent;
        [SerializeField]
        private int _score;
        public int Score => _score;


        public void GainScore(int score)
        {
            _score += score;
            OnScoreChangedEvent?.Invoke(_score);
        }
    }
}