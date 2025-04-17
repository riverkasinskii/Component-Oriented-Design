using System;
using UnityEngine;
using Zenject;

public sealed class CharacterPushController : IInitializable, IDisposable
{
    private readonly EntityProvider _characterProvider;
    private readonly IPushInput _pushInput;
    private readonly EnemyManager _entityManager;

    private PushComponent _pushComponent;
    private MoveComponent _moveComponent;
    private Transform _transform;

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
        _transform = _characterProvider.Value.Get<Transform>();

        _pushInput.OnInputInvoked += InputListener;
    }

    void IDisposable.Dispose()
    {
        _pushInput.OnInputInvoked -= InputListener;
    }

    private void InputListener()
    {
        var entities = _entityManager.GetEnemies();
        var direction = _moveComponent.CurrentDirection;

        _pushComponent.Push(entities, _transform, direction);
    }
}
