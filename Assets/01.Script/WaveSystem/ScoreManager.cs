using UnityEngine;

public class ScoreManager : MonoSingleton<ScoreManager>
{
    private int score;
    public int Score
    {
        get => score;
        set
        {
            score = value;
        }
    }
    public void PlusScore(int value)
    {
        Score += value;
        Debug.Log(score);
    }
}
