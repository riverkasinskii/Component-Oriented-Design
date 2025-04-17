using System;
using UnityEngine;
using Zenject;

public sealed class CharacterTossController : IInitializable, IDisposable
{
    private readonly EntityProvider _characterProvider;
    private readonly ITossInput _tossInput;
    private readonly EnemyManager _enemyManager;

    private TossComponent _tossComponent;    
    private Transform _transform;

    public CharacterTossController(EntityProvider characterProvider, ITossInput tossInput, EnemyManager enemyManager)
    {
        _characterProvider = characterProvider;
        _tossInput = tossInput;
        _enemyManager = enemyManager;
    }

    void IInitializable.Initialize()
    {
        _tossComponent = _characterProvider.Value.Get<TossComponent>();        
        _transform = _characterProvider.Value.Get<Transform>();

        _tossInput.OnInputInvoked += InputListener;
    }

    void IDisposable.Dispose()
    {
        _tossInput.OnInputInvoked -= InputListener;
    }

    private void InputListener(Vector2 direction)
    {
        var entities = _enemyManager.GetEnemies();        

        _tossComponent.Toss(entities, _transform, direction);
    }
}
