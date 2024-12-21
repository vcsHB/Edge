using System;
using Core;
using TMPro;
using UnityEngine;

public class BestScoreTExt : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _scoreText;


    private void Awake()
    {
        
        GameLog gameData = BGDJson.FromJson<GameLog>("GameData");
        _scoreText.text = "";
        if (gameData == null) return;
        if (gameData.bestScore == 0) return;
        
        _scoreText.text = $"BEST SCORE:{gameData.bestScore.ToString()}";
    }
}
