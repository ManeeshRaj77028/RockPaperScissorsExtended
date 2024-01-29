using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class HandManager : MonoBehaviour
{
    [SerializeField] private Hand[] hands;

    private Dictionary<Type, IHand> handsDictionary; // For faster accessing hands instance using type ( assuming there are chances of more hands being added )
    
    void Start()
    { 
        // We can use reflections and executing assembly here instead of manually adding all the hand classes to inspector
        InitialiseHands();
    }

    private void InitialiseHands()
    {
        hands = GetComponents<Hand>();
        PopulateHandsDictionary();
    }

    private void PopulateHandsDictionary()
    {
        handsDictionary = new Dictionary<Type, IHand>();
        for (int i = 0; i < hands.Length; i++)
        {
            handsDictionary.TryAdd(hands[i].GetType(), hands[i]);
        }
    }

    public (bool isWinner,bool isTie ,Type winnerType) GetWinner(Type handType1,Type handType2)
    {
        //Tie check
        if (handType1 == handType2) return (false, true, null);
        
        // Check for valid Hand
        IHand hand;
        if (!handsDictionary.TryGetValue(handType1, out hand) || !handsDictionary.ContainsKey(handType2)) // We could Log 
        {
            return (false, true, null);
        }
        
        // Winner calculations
        return CalculateWinner(hand,handType1,handType2); 
    }

    private (bool isWinner, bool isTie, Type winnerType) CalculateWinner(IHand hand,Type handType1,Type handType2)
    {
        Debug.Log("Calculating winner");
        var isHandWinning = hand.CheckCounters(handType2); // It can only win or lose , so we just need to check if hand has counters to win else it automatically loses.
        var winner = isHandWinning ? handType1 : handType2;
        return (isHandWinning, false, winner);
    }

    public IHand GetHand(Type handType)
    {
        IHand hand = default;
        handsDictionary.TryGetValue(handType, out hand);
        return hand;
    }
}
