using System;
using UnityEngine;
using Zenject;

public sealed class CharacterPushController : IInitializable, IDisposable, ITickable
{
    private readonly EntityProvider _characterProvider;
    private readonly IPushInput _pushInput;
    private readonly EnemyManager _entityManager;
    private readonly Timer _timer = new();

    private PushComponent _pushComponent;
    private MoveComponent _moveComponent;
    private AudioComponent _audioComponent;

    private Transform _transform;
    private bool timerState = false;
        
    public CharacterPushController(EntityProvider characterProvider, IPushInput pushInput, EnemyManager entityManager)
    {
        _characterProvider = characterProvider;
        _pushInput = pushInput;
        _entityManager = entityManager;
    }
        
    void IInitializable.Initialize()
    {
        _pushComponent = _characterProvider.Value.Get<PushComponent>();
        _moveComponent = _characterProvider.Value.Get<MoveComponent>();  
        _audioComponent = _characterProvider.Value.Get<AudioComponent>();
        _transform = _characterProvider.Value.Get<Transform>();
                
        _pushInput.OnInputInvoked += InputListener;
        _timer.TimerStarted += TimerStateChanged;
        _timer.TimerFinished += TimerStateChanged;
        _pushComponent.OnPushed += OnPushed;
    }

    void IDisposable.Dispose()
    {
        _pushInput.OnInputInvoked -= InputListener;
        _timer.TimerStarted -= TimerStateChanged;
        _timer.TimerFinished -= TimerStateChanged;
        _pushComponent.OnPushed -= OnPushed;
    }

    private void OnPushed()
    {
        _audioComponent.PlayOneShot(AudioData.Push);
    }

    private void TimerStateChanged(bool value)
    {
        timerState = value;
    }

    private void InputListener()
    {
        var entities = _entityManager.GetEnemies();
        var direction = _moveComponent.CurrentDirection;

        if (timerState)
        {
            return;
        }
        _pushComponent.Push(entities, _transform, direction);
        _timer.Start(_pushComponent.PushCooldown);
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
