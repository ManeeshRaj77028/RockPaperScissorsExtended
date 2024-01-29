using UnityEngine;

// Decorator Pattern to use as a shell around existing hand functionality
public class CounterBehaviour : MonoBehaviour,ICounterBehaviour // Mono behaviour for inspector display purposes , can be a non monobehaviour
{
    protected Hand theCounterer;
    protected Hand theCountered;
    protected HandManager handManager;
    protected string CounterMessage;

    public IHand GetCounterer => theCounterer;
    public IHand GetCountered => theCountered;
    
    public virtual void Initialize(HandManager handManager) => this.handManager = handManager;
    
    public virtual string Counters()
    {
        Debug.Log(CounterMessage);
        return CounterMessage;
    }

    // For optional run time counter changes
    public virtual void SetCounter(IHand theCounterer,IHand theCountered)
    {
        this.theCounterer = (Hand) theCounterer; // Generally faster casting when succeeds, we can use "as" we need to avoid invalid cast exception
        this.theCountered = (Hand) theCountered;
        
        theCounterer.SetCounters(theCountered.GetSelfType());
        theCountered.SetCounteredBy(theCounterer.GetSelfType());
    }
    
}
