using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

// Strategy pattern to use different hand strategies
public class Hand : MonoBehaviour,IHand // Mono behaviour for inspector display purposes , can be a non monobehaviour
{
    protected Type selfType;
    protected HashSet<Type> handCounters = new(); // For unique elements and faster retrieval
    protected HashSet<Type> handCounteredBy = new();

    protected List<Type> listOfHandCounters;
    protected List<Type> listOfhandCounteredBy;

    public virtual bool CheckCounters(Type counter) => handCounters.Contains(counter);
    public virtual bool CheckCounteredBy(Type counteredBy) => handCounteredBy.Contains(counteredBy);
    public virtual void SetCounters(params Type[] counters) => PopulateHandCounters(counters);
    public virtual void SetCounteredBy(params Type[] counteredBy) => PopulateHandCounteredBy(counteredBy);
    public List<Type> GetCounters() => listOfHandCounters; // Alternate but expensive casting (List<Type>) handCounters.AsReadOnlyList();
    public List<Type> GetCounteredBy() => listOfhandCounteredBy;
    public virtual Type GetSelfType() => selfType;

    private void PopulateHandCounters(Type[] counters)
    {
        handCounters.AddRange(counters);
        listOfHandCounters = handCounters.ToList();
    }

    private void PopulateHandCounteredBy(Type[] counteredBy)
    {
        handCounteredBy.AddRange(counteredBy);
        listOfhandCounteredBy = handCounteredBy.ToList();
    }
}
