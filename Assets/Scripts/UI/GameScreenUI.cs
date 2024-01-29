using System;
using TMPro;
using UnityEngine;

public sealed class GameScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private SettingScreenUI settingScreenUI;
    [SerializeField] private TextMeshProUGUI highScoreText;
    
    private Action PlayButtonCallback;

    private void OnEnable()
    {
        ScoreManager.HighscoreUpdated += ShowHighScore;
        ShowHighScore(0);
    }

    private void OnDisable()
    {
        ScoreManager.HighscoreUpdated -= ShowHighScore;
    }

    public void OpenUI()
    {
        uiPanel.SetActive(true);
        ShowHighScore(0);
    }

    public void CloseUI() => uiPanel.SetActive(false);

    public void SetPlayButtonCallback(Action callback) => PlayButtonCallback = callback;
    public void OnPlayButtonClicked() => PlayButtonCallback?.Invoke();
    public void OnSettingButtonClicked() => settingScreenUI.OpenUI();
    public void ExitApp() => Application.Quit();
    private void ShowHighScore(int _) => highScoreText.text = "High Score\n"+ScoreManager.GetHighScore;
}
