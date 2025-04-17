using System;
using UnityEngine;
using Zenject;

public sealed class JumpComponent : ITickable, IInitializable, IDisposable
{
    public event Action OnJumped;
            
    private readonly float _jumpForce;
    private readonly float _coolDown;

    private readonly Timer _timer = new();

    private bool timerHasStarted = false;

    public JumpComponent(float jumpForce, float coolDown)
    {
        _jumpForce = jumpForce;
        _coolDown = coolDown;        
    }

    private readonly Condition _condition = new();
        
    public void Jump(Vector2 direction, Rigidbody2D rb)
    {
        if (_condition.IsTrue() && !_timer.IsActive)
        {
            rb.AddForce(new Vector2(0, direction.y * _jumpForce), ForceMode2D.Impulse);            
            OnJumped?.Invoke();

            _timer.Start(_coolDown);
            timerHasStarted = true;
        }        
    }

    public void AddCondition(Func<bool> condition)
    {
        _condition.AddCondition(condition);
    }

    void ITickable.Tick()
    {
        if (timerHasStarted)
        {
            _timer.Tick(Time.deltaTime);
        }       
    }

    void IInitializable.Initialize()
    {
        _timer.TimerFinished += TimerFinished;
    }

    void IDisposable.Dispose()
    {
        _timer.TimerFinished -= TimerFinished;
    }

    private void TimerFinished(bool value)
    {
        timerHasStarted = value;
    }
}
