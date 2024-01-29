using System;

public interface IBot
{
    void SetIntelligenceLevel(int intelligence);
    Type CalculateHand(Type opponentHand,HandManager handManager);
}
