using System.Collections.Generic;
using Random = UnityEngine.Random;

public static class Utilities 
{
    public static T GetRandomElement<T>(this List<T> list)
    {
        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
