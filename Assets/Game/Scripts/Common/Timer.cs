using System;
using UnityEngine;

public class Timer
{    
    public event Action<float> TimerValueChanged;
    public event Action<bool> TimerStarted;
    public event Action<bool> TimerFinished;       
    public bool IsActive { get; private set; }
    public bool IsPaused { get; private set; }
    public float RemainingSeconds { get; private set; }
       
    public Timer()
    {

    }

    public void SetTime(float seconds)
    {        
        RemainingSeconds = seconds;
        TimerValueChanged?.Invoke(RemainingSeconds);
    }

    public void Start()
    {
        if (IsActive)
            return;

        if (Math.Abs(RemainingSeconds) < Mathf.Epsilon)
        {
#if DEBUG
            Debug.LogError("TIMER: You are trying start timer with remaining seconds equal 0.");
#endif
            TimerFinished?.Invoke(false);
        }

        IsActive = true;
        IsPaused = false;
               
        TimerStarted?.Invoke(true);
        TimerValueChanged?.Invoke(RemainingSeconds);
    }

    public void Start(float seconds)
    {
        if (IsActive)
            return;

        SetTime(seconds);
        Start();        
    }
        
    public void Pause()
    {
        if (IsPaused || !IsActive)
            return;

        IsPaused = true;        

        TimerValueChanged?.Invoke(RemainingSeconds);
    }

    public void Unpause()
    {
        if (!IsPaused || !IsActive)
            return;

        IsPaused = false;        

        TimerValueChanged?.Invoke(RemainingSeconds);
    }

    public void Stop()
    {
        if (IsActive)
        {
            RemainingSeconds = 0f;
            IsActive = false;
            IsPaused = false;

            TimerValueChanged?.Invoke(RemainingSeconds);
            TimerFinished?.Invoke(false);            
        }
    }
        
    private void CheckFinish()
    {
        if (RemainingSeconds <= 0f)
        {
            Stop();
        }
    }

    private void NotifyAboutTimePassed()
    {
        if (RemainingSeconds >= 0f)
        {
            TimerValueChanged?.Invoke(RemainingSeconds);
        }
    }

    public void Tick(float deltaTime)
    {
        RemainingSeconds -= deltaTime;        
        NotifyAboutTimePassed();
        CheckFinish();
    }
}
