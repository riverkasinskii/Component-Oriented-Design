using System;
using UnityEngine;

public sealed class Timer
{
    public event Action<float> TimerValueChanged;
    public event Action<bool> TimerStarted;
    public event Action<bool> TimerFinished;

    public bool IsActive { get; private set; }
    public bool IsPaused { get; private set; }    

    private float _remainingSeconds;               
    
    public void Start(float seconds)
    {
        if (IsActive)
            return;

        SetTime(seconds);
        StartTimer();
    }
        
    public void Pause()
    {
        if (IsPaused || !IsActive)
            return;

        IsPaused = true;

        TimerValueChanged?.Invoke(_remainingSeconds);
    }

    public void Unpause()
    {
        if (!IsPaused || !IsActive)
            return;

        IsPaused = false;

        TimerValueChanged?.Invoke(_remainingSeconds);
    }

    public void Stop()
    {
        if (IsActive)
        {
            _remainingSeconds = 0f;
            IsActive = false;
            IsPaused = false;

            TimerValueChanged?.Invoke(_remainingSeconds);
            TimerFinished?.Invoke(false);
        }
    }

    private void SetTime(float seconds)
    {
        _remainingSeconds = seconds;
        TimerValueChanged?.Invoke(seconds);
    }


    private void StartTimer()
    {
        if (IsActive)
            return;

        if (Math.Abs(_remainingSeconds) < Mathf.Epsilon)
        {
#if DEBUG
            Debug.LogError("TIMER: You are trying start timer with remaining seconds equal 0.");
#endif
            TimerFinished?.Invoke(false);
        }

        IsActive = true;
        IsPaused = false;

        TimerStarted?.Invoke(true);

        TimerValueChanged?.Invoke(_remainingSeconds);
    }

    private void CheckFinish()
    {
        if (_remainingSeconds <= 0f)
        {
            Stop();
        }
    }

    private void NotifyAboutTimePassed()
    {
        if (_remainingSeconds >= 0f)
        {
            TimerValueChanged?.Invoke(_remainingSeconds);
        }
    }

    public void Tick()
    {
        _remainingSeconds -= Time.deltaTime;        
        NotifyAboutTimePassed();
        CheckFinish();
    }
}
