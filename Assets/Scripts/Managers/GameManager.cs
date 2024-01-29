using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class GameManager : MonoBehaviour
{
    [SerializeField] private GameScreenUI gameScreenUI;
    [SerializeField] private HandManager handManager;
    [SerializeField] private CounterManager counterManager;
    [SerializeField] private RoundManager roundManager;
    [SerializeField] private ScoreManager scoreManager;
    
    private IPlayer player;
    private IBot bot;
    private Timer timer;

    private bool isGameRunning;
    
    private WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();
    private IEnumerator Start()
    {
        yield return waitForEndOfFrame;
        InitializeGame();
        InitializeUI();
    }

    private void InitializeGame()
    {
        counterManager.InitializeCountersForHands(handManager);
        player = CreatePlayer();
        bot = CreateBot(SaveSystem.GetBotIntelligence());
        timer = CreateTimer();
        roundManager.InjectDependencies(player,bot,timer,handManager,counterManager,scoreManager);
    }

    private void InitializeUI() => gameScreenUI.SetPlayButtonCallback(StartGame);

    private IPlayer CreatePlayer()
    {
        var obj = new GameObject("Player", typeof(Player));
        return obj.GetComponent<Player>();
    }

    private IBot CreateBot(int intelligence)
    {
        var obj = new GameObject("Bot", typeof(Bot));
        var bot = obj.GetComponent<Bot>();
        bot.SetIntelligenceLevel(intelligence);
        return bot;
    }

    private Timer CreateTimer()
    {
        var obj = new GameObject("Timer", typeof(Timer));
        return obj.GetComponent<Timer>();
    }

    private IEnumerator PlayGame()
    {
        var waitWhileRoundIsRunning = new WaitWhile(roundManager.CheckIsRoundRunning);
        roundManager.StartRounds();
        while (isGameRunning)
        {
            StartCoroutine(roundManager.PlayRound());
            yield return waitWhileRoundIsRunning;
            ProcessRoundStatus(roundManager.GetRoundResult());
        }
        // When game stops running end the game
        Reload();
    }

    void ProcessRoundStatus((RoundStatus roundStatus,string message) roundResult) // Can be converted to state machine
    {
        switch (roundResult.roundStatus)
        {
            case RoundStatus.RanOutOfTime :
            case RoundStatus.Lost: isGameRunning = false; break;
            case RoundStatus.Won: scoreManager.IncreaseScore(1); break;
            case RoundStatus.Tie: break;
        }
    }

    public void StartGame()
    {
        isGameRunning = true;
        StartCoroutine(PlayGame());
    }

    public void EndGame() => isGameRunning = false;
    public void Reload() => SceneManager.LoadScene(0);
}
