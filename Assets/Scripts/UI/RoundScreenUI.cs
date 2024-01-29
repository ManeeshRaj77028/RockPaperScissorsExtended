using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public sealed class RoundScreenUI : MonoBehaviour
{
    [SerializeField] private GameObject uiPanel;
    [SerializeField] private TextMeshProUGUI roundResultText;
    [SerializeField] private TextMeshProUGUI counterMessageText;

    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private Image timerFillImage;

    [SerializeField] private GameObject botPlayedText;
    [SerializeField] private AnimateHandsData playerHandUI;
    [SerializeField] private AnimateHandsData botHandUI;
    
    public void OpenUI()
    {
        uiPanel.SetActive(true);
    }

    public void CloseUI()
    {
        uiPanel.SetActive(false);
    }
    
    public RoundScreenUI UpdateRoundResult(string roundResultMessage)
    {
        roundResultText.text = roundResultMessage;
        return this;
    }

    public RoundScreenUI UpdateCounterMessageText(string counterMessage)
    {
        counterMessageText.text = counterMessage;
        return this;
    }

    public RoundScreenUI UpdateScoreText((int score,bool isHighScore) scoreInfo)
    {
        scoreText.text = (scoreInfo.isHighScore? "NEW HIGH SCORE" : "SCORE") +"\n"+ scoreInfo.score;
        return this;
    }

    public void UpdateTimerText(float timer,float endTime)
    {
        timerText.text = ((int)timer).ToString();
        timerFillImage.fillAmount = timer / endTime;
    }

    public void UpdatePlayerHandAndBotHand(Type playerHand,Type botHand)
    {
        playerHandUI.SetHandType(playerHand);
        botHandUI.SetHandType(botHand);
        
        playerHandUI.Animate();
        botHandUI.Animate();
        botPlayedText.SetActive(true);
    }

    public void ResetAnimations()
    {
        playerHandUI.Reset();
        botHandUI.Reset();
        botPlayedText.SetActive(false);
    }
    
    [Serializable]
    public class AnimateHandsData
    {
        public TextMeshProUGUI handText;
        public GameObject handUI;
        public Vector3 fromPosition, toPosition;
        public Vector3 fromScale, toScale;
        public float duration;

        public void SetHandType(Type handType) => handText.text = handType.FullName;
        public void Animate()
        {
            LeanTween.moveLocal(handUI, toPosition, duration).setEaseOutBounce();
            LeanTween.scale(handUI, toScale, duration).setEaseOutBounce();
        }
        public void Reset()
        {
            handUI.transform.localPosition = fromPosition;
            handUI.transform.localScale = fromScale;
        }
    }
    
}



