using System;
using System.Collections.Generic;

public interface IHand // Potential interface seggregation ?
{
    Type GetSelfType();
    bool CheckCounters(Type counter);
    bool CheckCounteredBy(Type counteredBy);
    void SetCounters(params Type[] counters);
    void SetCounteredBy(params Type[] counterBy);

    List<Type> GetCounters();
    List<Type> GetCounteredBy();
}
