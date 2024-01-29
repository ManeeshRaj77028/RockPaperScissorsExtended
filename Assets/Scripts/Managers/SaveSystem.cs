using UnityEngine;

public sealed class SaveSystem
{
    public const string HIGH_SCORE = "highScore";
    public const string BOT_INTELLIGENCE = "botIntelligence";

    public static void SaveHighScore(int score)
    {
        PlayerPrefs.SetInt(HIGH_SCORE,score);
        PlayerPrefs.Save();
    }

    public static void SaveBotIntelligence(int intelligence)
    {
        PlayerPrefs.SetInt(BOT_INTELLIGENCE,intelligence);
        PlayerPrefs.Save();
    }

    public static int GetHighScore()
    {
        return PlayerPrefs.GetInt(HIGH_SCORE,0);
    }

    public static int GetBotIntelligence()
    {
        return PlayerPrefs.GetInt(BOT_INTELLIGENCE,50);
    }
}
