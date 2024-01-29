using System;

public interface IPlayer
{
    void SetPlayerHand(Type hand);
    Type GetPlayedHand();
    bool IsHandPlayed();
    void Reset();
}
