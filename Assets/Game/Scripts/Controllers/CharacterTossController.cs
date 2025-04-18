using System;
using UnityEngine;
using Zenject;

public sealed class CharacterTossController : IInitializable, IDisposable, ITickable
{   
    private readonly EntityProvider _entityProvider;        
    private readonly ITossInput _tossInput;        
    private readonly EnemyManager _enemyManager;
    private readonly Timer _timer = new();

    private TossComponent _tossComponent;   
    private AudioComponent _audioComponent;
    private Transform _transform;
    private bool timerState = false;

    public CharacterTossController(EntityProvider entityProvider, ITossInput tossInput, EnemyManager enemyManager)
    {
        _entityProvider = entityProvider;
        _tossInput = tossInput;
        _enemyManager = enemyManager;
    }
        
    void IInitializable.Initialize()
    {
        _tossComponent = _entityProvider.Value.Get<TossComponent>();  
        _audioComponent = _entityProvider.Value.Get<AudioComponent>();
        _transform = _entityProvider.Value.Get<Transform>();                
        _tossInput.OnInputInvoked += InputListener;

        _timer.TimerStarted += TimerStateChanged;
        _timer.TimerFinished += TimerStateChanged;
        _tossComponent.OnTossed += OnTossed;
    }

    void IDisposable.Dispose()
    {
        _tossInput.OnInputInvoked -= InputListener;

        _timer.TimerStarted -= TimerStateChanged;
        _timer.TimerFinished -= TimerStateChanged;
        _tossComponent.OnTossed -= OnTossed;
    }

    private void OnTossed()
    {
        _audioComponent.PlayOneShot(AudioData.Toss);
    }

    private void TimerStateChanged(bool value)
    {
        timerState = value;
    }

    private void InputListener()
    {
        var entities = _enemyManager.GetEnemies();
                
        if (timerState)
        {
            return;
        }

        _tossComponent.TryToss(entities, _transform);
        _timer.Start(_tossComponent.TossCooldown);
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
