using UnityEngine;

public sealed class Timer : MonoBehaviour
{
    private float endTime;
    private float timer;
    
    private bool isAutoEnd = true;
    private bool isTimerCompleted;
    private bool runTimer;

    private void Update()
    {
        RunTimer();
    }
    
    private void RunTimer()
    {
        if (!runTimer) return;
        
        timer += Time.deltaTime;
        isTimerCompleted = timer > endTime;
        runTimer = !(isAutoEnd && isTimerCompleted);
    }

    public Timer StartTimer()
    {
        runTimer = true;
        return this;
    }
    
    public Timer SetTimer(float endTime,bool isReset=true)
    {
        if(isReset) ResetTimer();
        this.endTime = endTime;
        return this;
    }
    
    public Timer SetAutoEnd(bool autoEnd)
    {
        isAutoEnd = autoEnd;
        return this;
    }
    
    public void ResetTimer()
    {
        timer = 0;
        isTimerCompleted = false;
    }

    public void StopTimer() => runTimer = false;
    public float GetTimer() => timer;
    public float GetEndTime() => endTime;
    public bool IsTimerCompleted() => isTimerCompleted;
}
