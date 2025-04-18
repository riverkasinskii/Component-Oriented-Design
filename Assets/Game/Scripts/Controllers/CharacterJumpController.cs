using System;
using UnityEngine;
using Zenject;

public sealed class CharacterJumpController : IInitializable, IDisposable, ITickable
{
    private readonly EntityProvider _character;
    private readonly IJumpInput _jumpInput;
    private readonly Timer _timer = new();

    private JumpComponent _jumpComponent;
    private AudioComponent _audioComponent;

    private Rigidbody2D _rb;
    private bool timerState = false;

    public CharacterJumpController(EntityProvider character, IJumpInput jumpInput)
    {
        _character = character;
        _jumpInput = jumpInput;
    }
        
    void IInitializable.Initialize()
    {
        _jumpComponent = _character.Value.Get<JumpComponent>();
        _audioComponent = _character.Value.Get<AudioComponent>();
        _rb = _character.Value.Get<Rigidbody2D>();


        _jumpInput.OnInputInvoked += OnInputListener;
        _jumpComponent.OnJumped += Jumped;
        _timer.TimerStarted += TimerStateChanged;
        _timer.TimerFinished += TimerStateChanged;
    }

    void IDisposable.Dispose()
    {
        _jumpInput.OnInputInvoked -= OnInputListener;
        _jumpComponent.OnJumped -= Jumped;
        _timer.TimerStarted -= TimerStateChanged;
        _timer.TimerFinished -= TimerStateChanged;
    }

    private void TimerStateChanged(bool value)
    {
        timerState = value;
    }

    private void Jumped()
    {
        _audioComponent.PlayOneShot(AudioData.Jump);
    }

    private void OnInputListener(Vector2 vector)
    {
        if (timerState)
        {
            return;
        }

        _jumpComponent.Jump(vector, _rb);
        _timer.Start(_jumpComponent.JumpCooldown);
    }

    void ITickable.Tick()
    {
        if (!timerState)
        {
            return;
        }
        _timer.Tick();
    }
}
