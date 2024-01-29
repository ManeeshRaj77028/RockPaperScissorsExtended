using UnityEngine;

public sealed class Vaporize : CounterBehaviour
{
    [SerializeField] private string counterMessage;
    
    public override void Initialize(HandManager handManager)
    {
        base.Initialize(handManager);
        CounterMessage = counterMessage;
        SetCounter(handManager.GetHand(typeof(Spock)),handManager.GetHand(typeof(Rock)));    
    }

    public override string Counters()
    {
        var message = base.Counters();
        return $"{theCounterer.GetSelfType()} {message} {theCountered.GetSelfType()}";
    }
}
