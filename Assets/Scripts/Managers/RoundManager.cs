using System;
using System.Collections;
using UnityEngine;

public sealed class RoundManager : MonoBehaviour
{
    [SerializeField] private RoundScreenUI roundScreenUI;
    
    private HandManager handManager;
    private CounterManager counterManager;
    private ScoreManager scoreManager;
    
    private IPlayer player;
    private IBot bot;
    private Timer timer;
    
    private bool isRoundRunning;
    private (RoundStatus roundStatus, string message) roundResults;
    
    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private WaitForSeconds waitFor5Seconds = new WaitForSeconds(5f);
    private WaitForSeconds waitFor3Seconds = new WaitForSeconds(3f);
    
    public void InjectDependencies(IPlayer player,IBot bot ,Timer timer,HandManager handManager,CounterManager counterManager,ScoreManager scoreManager)
    {
        this.player = player;
        this.bot = bot;
        this.timer = timer;
        this.handManager = handManager;
        this.counterManager = counterManager;
        this.scoreManager = scoreManager;
    }

    public void StartRounds()
    {
        roundScreenUI.OpenUI();
    }

    private void ResetRound()
    {
        player.Reset();
        roundScreenUI.UpdateRoundResult("").UpdateCounterMessageText("").UpdateScoreText(scoreManager.GetScore()).ResetAnimations();
    }
    
    public IEnumerator PlayRound()
    {
        isRoundRunning = true;
        ResetRound();
        timer.SetTimer(5).StartTimer();
        while (!( timer.IsTimerCompleted() || player.IsHandPlayed() ) )
        {
            roundScreenUI.UpdateTimerText(timer.GetTimer(),timer.GetEndTime());
            yield return waitForEndOfFrame;
        }

        timer.StopTimer();
        if (timer.IsTimerCompleted()) roundResults = HandleOnTimerCompleted();
        else if (player.IsHandPlayed())
        {
            roundResults = HandleOnPlayerPlayed();
            yield return waitFor3Seconds;
        }
        
        roundScreenUI.UpdateRoundResult("You "+roundResults.roundStatus).UpdateCounterMessageText(roundResults.message);
        yield return waitFor3Seconds;
        isRoundRunning = false;
    }

    private (RoundStatus roundStatus,string message) HandleOnTimerCompleted()
    {
        return (RoundStatus.RanOutOfTime, "You ran out of time"); // Can move these messages to scriptable object to avoid modifying scripts
    }

    private (RoundStatus roundStatus,string message) HandleOnPlayerPlayed()
    {
        var playerHand = player.GetPlayedHand();
        var botHand = bot.CalculateHand(playerHand,handManager);
        roundScreenUI.UpdatePlayerHandAndBotHand(playerHand,botHand);
        
        var winnerDetails = handManager.GetWinner(playerHand, botHand);
        if(!winnerDetails.isTie) return HandleWinner(winnerDetails.winnerType,playerHand,botHand);
        
        return HandleTie();
    }

    private (RoundStatus roundStatus,string message) HandleWinner(Type winnerHand ,Type playerHand ,Type botHand)
    {
        bool playerWon = winnerHand == playerHand;
        Type loserHand = playerWon ? botHand : playerHand;
        RoundStatus roundStatus = playerWon ? RoundStatus.Won : RoundStatus.Lost;
        
        var counterDeatails = counterManager.GetCounter(winnerHand, loserHand);
        var counterMessage = counterDeatails.Counters();
        return (roundStatus,counterMessage);
    }

    private (RoundStatus roundStatus,string message) HandleTie()
    {
        return (RoundStatus.Tie, "You have Tied"); // Can move these messages to scriptable object to avoid modifying scripts 
    }

    public bool CheckIsRoundRunning() => isRoundRunning;

    public (RoundStatus roundStatus, string message) GetRoundResult() => roundResults;
}

public enum RoundStatus
{
    Won, Lost, Tie,RanOutOfTime
}
