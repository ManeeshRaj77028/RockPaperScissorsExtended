using System;
using UnityEngine;

public sealed class ScoreManager : MonoBehaviour
{
    private int currentScore;
    private int currentHighScore;
    private bool isNewHighScore;

    public static Action<int> HighscoreUpdated;
    public static int GetHighScore => SaveSystem.GetHighScore();
    private void OnEnable()
    {
        HighscoreUpdated += SetHighScore;
        Initialise();
    }

    private void OnDisable()
    {
        HighscoreUpdated -= SetHighScore;
    }

    public void Initialise()
    {
        currentScore = 0;
        currentHighScore = SaveSystem.GetHighScore();
    }

    public (int score,bool isHigh) GetScore()
    {
        return (currentScore,isNewHighScore);
    }
    
    public void SetScore(int score)
    {
        currentScore = score;
        CheckForNewHighScore();
    }

    public void ResetScore()
    {
        currentScore = 0;
    }
    
    public void IncreaseScore(int count)
    {
        currentScore += 1;
        CheckForNewHighScore();
    }

    private void CheckForNewHighScore()
    {
        if (currentScore > currentHighScore)
        {
            SetHighScore(currentScore);
        }
    }

    private void SetHighScore(int score)
    {
        isNewHighScore = true;
        currentHighScore = score;
        SaveSystem.SaveHighScore(currentHighScore);
    }
    
}
