using UnityEngine;

public sealed class Cover : CounterBehaviour
{
    [SerializeField] private string counterMessage;
    
    public override void Initialize(HandManager handManager)
    {
        base.Initialize(handManager);
        CounterMessage = counterMessage;
        SetCounter(handManager.GetHand(typeof(Paper)),handManager.GetHand(typeof(Rock)));    
    }

    public override string Counters()
    {
        var message = base.Counters();
        return $"{theCounterer.GetSelfType()} {message} {theCountered.GetSelfType()}";
    }
    
}
