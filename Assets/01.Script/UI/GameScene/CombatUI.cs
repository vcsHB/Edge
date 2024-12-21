using DG.Tweening;
using Managers;
using TMPro;
using UnityEngine;

namespace UIManage
{

    public class CombatUI : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _currentScore;
        [SerializeField] private Vector3 _impactScale;

        private void Start()
        {
            ScoreManager.Instance.OnScoreChangedEvent += HandleRefreshScore;
            
        }


        public void HandleRefreshScore(int score)
        {
            _currentScore.text = score.ToString();
            _currentScore.transform.localScale = _impactScale;
            _currentScore.transform.DOScale(Vector3.one, 0.2f).SetEase(Ease.InBack);
        }

    }

}