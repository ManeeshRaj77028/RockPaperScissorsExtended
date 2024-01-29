using UnityEngine;

public sealed class HardCrush : CounterBehaviour
{
    [SerializeField] private string counterMessage;
    
    public override void Initialize(HandManager handManager)
    {
        base.Initialize(handManager);
        CounterMessage = counterMessage;
        SetCounter(handManager.GetHand(typeof(Rock)),handManager.GetHand(typeof(Scissor)));    
    }

    public override string Counters()
    {
        var message = base.Counters();
        return $"{theCounterer.GetSelfType()} {message} {theCountered.GetSelfType()}";
    }
}
