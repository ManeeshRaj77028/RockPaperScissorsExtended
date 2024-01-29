using System;
using UnityEngine;

public class HandUIBehaviour : MonoBehaviour
{
    protected Type handType;

    public Type GetHandType()
    {
        return handType;
    }
    
    public virtual void OnSelected()
    {
        Player.SelectPlayerHand(handType);
    }
}
