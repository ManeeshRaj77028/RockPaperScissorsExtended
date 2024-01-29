using UnityEngine;

public sealed class Decapitate : CounterBehaviour
{
    [SerializeField] private string counterMessage;
    
    public override void Initialize(HandManager handManager)
    {
        base.Initialize(handManager);
        CounterMessage = counterMessage;
        SetCounter(handManager.GetHand(typeof(Scissor)),handManager.GetHand(typeof(Lizard)));    
    }

    public override string Counters()
    {
        var message = base.Counters();
        return $"{theCounterer.GetSelfType()} {message} {theCountered.GetSelfType()}";
    }
}
