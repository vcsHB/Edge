using Managers;
using TMPro;
using UnityEngine;

namespace UIManage
{

    public class CombatUI : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _currentScore;

        private void Start()
        {
            ScoreManager.Instance.OnScoreChangedEvent += HandleRefreshScore;
            
        }


        public void HandleRefreshScore(int score)
        {
            _currentScore.text = score.ToString();
        }

    }

}