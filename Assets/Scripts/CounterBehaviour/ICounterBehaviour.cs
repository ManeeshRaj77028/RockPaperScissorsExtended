public interface ICounterBehaviour
{
    void Initialize(HandManager handManager);
    string Counters();
    void SetCounter(IHand theCounterer, IHand theCountered);
}
