using Core;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UIManage
{
    
    
    public class GameOverPanel : UIPanel
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        private int _endScore;

        public override void Open()
        {
            base.Open();
            Time.timeScale = 0f;
            SetScoreText();
        }

        public void SetScoreText()
        {
            _endScore = ScoreManager.Instance.Score;
            _scoreText.text = $"SCORE : {_endScore.ToString()}";
        }


        public void HandleExitGameScene()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScene");
            // 저장 추가
            GameLog gameData = BGDJson.FromJson<GameLog>("GameData");
            if (gameData == null)
            {
                gameData = new GameLog();
                gameData.bestScore = _endScore;
                BGDJson.ToJson<GameLog>(gameData, "GameData", true);
            }
            else if (gameData.bestScore < _endScore)
            {
                gameData.bestScore = _endScore;
                BGDJson.ToJson<GameLog>(gameData, "GameData", true);
                
            }
        }

        public void HandleExit()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("TitleScene");
        }
        
    }

}