using System;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class Bot : MonoBehaviour,IBot
{
    private int intelligenceLevel;

    public static Action<int> IntelligenceChanged;
    private void OnEnable()
    {
        IntelligenceChanged += SetIntelligenceLevel;
    }

    private void OnDisable()
    {
        IntelligenceChanged += SetIntelligenceLevel;
    }
    
    private bool CalculateWinningProbability()
    {
        int probability = Random.Range(0, 101);
        return intelligenceLevel > probability;
    }
    
    public void SetIntelligenceLevel(int intelligence) => intelligenceLevel = intelligence;

    public Type CalculateHand(Type opponentHand,HandManager handManager)
    {
        bool canWin = CalculateWinningProbability();
        var hand = handManager.GetHand(opponentHand);
        var botHand = canWin ? hand.GetCounteredBy().GetRandomElement() : hand.GetCounters().GetRandomElement();
        return botHand;
    }
}
