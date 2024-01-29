using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class SettingScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private Slider botIntelligenceSlider;
    [SerializeField] private TextMeshProUGUI sliderValueText;
    
    public void OpenUI()
    {
        uiPanel.SetActive(true);
        Initialise();
    }

    public void CloseUI() => uiPanel.SetActive(false);
    public void Initialise() => botIntelligenceSlider.value = SaveSystem.GetBotIntelligence();
    
    public void OnHighScoreResetClicked() => ScoreManager.HighscoreUpdated?.Invoke(0);

    public void OnBotIntelligenceSliderValueChange(float sliderValue)
    {
        int intelligence = (int) sliderValue;
        Bot.IntelligenceChanged?.Invoke(intelligence);
        SaveSystem.SaveBotIntelligence(intelligence);
        sliderValueText.text = intelligence.ToString();
    }

    
}
