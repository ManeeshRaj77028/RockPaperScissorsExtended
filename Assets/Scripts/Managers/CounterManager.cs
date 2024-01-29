using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class CounterManager : MonoBehaviour
{
    [SerializeField] private CounterBehaviour[] counterBehaviours;

    private Dictionary<(Type, Type), ICounterBehaviour> counterBehaviourDictionary;

    public void InitializeCountersForHands(HandManager handManager)
    {
        counterBehaviours = GetComponents<CounterBehaviour>();
        InitializeCounterBehavioursAndAddToDict(handManager);
    }

    private void InitializeCounterBehavioursAndAddToDict(HandManager handManager)
    {
        counterBehaviourDictionary = new Dictionary<(Type, Type), ICounterBehaviour>();
        for (int i = 0; i < counterBehaviours.Length; i++)
        {
            counterBehaviours[i].Initialize(handManager);
            counterBehaviourDictionary.TryAdd(
                (counterBehaviours[i].GetCounterer.GetSelfType(), counterBehaviours[i].GetCountered.GetSelfType()),
                counterBehaviours[i]);
        }
    }

    public ICounterBehaviour GetCounter(Type countererType,Type counteredType)
    {
        ICounterBehaviour counterBehaviour = default;
        counterBehaviourDictionary.TryGetValue((countererType, counteredType), out counterBehaviour);
        return counterBehaviour;
    }
}
 