using System;
using UnityEngine;

public sealed class Player : MonoBehaviour,IPlayer
{
    private bool isHandplayed;
    private Type playedHand;

    public static Action<Type> SelectPlayerHand;

    private void OnEnable()
    {
        SelectPlayerHand += SetPlayerHand; // using as a multicast delegate
    }

    private void OnDisable()
    {
        SelectPlayerHand -= SetPlayerHand;
    }

    public void SetPlayerHand(Type hand)
    {
        isHandplayed = true;
        playedHand = hand;
    }
    public Type GetPlayedHand() => playedHand;
    public bool IsHandPlayed() => isHandplayed;
    public void Reset() => isHandplayed = false;
}
