using UnityEngine;

public sealed class SoftCrush : CounterBehaviour
{
    [SerializeField] private string counterMessage;
    
    public override void Initialize(HandManager handManager)
    {
        base.Initialize(handManager);
        CounterMessage = counterMessage;
        SetCounter(handManager.GetHand(typeof(Rock)),handManager.GetHand(typeof(Lizard)));    
    }

    public override string Counters()
    {
        var message = base.Counters();
        return $"{theCounterer.GetSelfType()} {message} {theCountered.GetSelfType()}";
    }
}
