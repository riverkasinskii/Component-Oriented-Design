using System;
using UnityEngine;
using Zenject;

public sealed class SnakeRotateController : IInitializable, IDisposable
{
    private readonly EntityProvider _entity;    
    private RotateComponent _rotateComponent;
    private MoveComponent _moveComponent;

    public SnakeRotateController(EntityProvider entity)
    {
        _entity = entity;        
    }

    void IInitializable.Initialize()
    {
        _rotateComponent = _entity.Value.Get<RotateComponent>();
        _moveComponent = _entity.Value.Get<MoveComponent>();

        _moveComponent.OnDirectionChanged += DirectionChanged;
    }

    void IDisposable.Dispose()
    {
        _moveComponent.OnDirectionChanged -= DirectionChanged;
    }

    private void DirectionChanged(Vector2 vector)
    {
        _rotateComponent.Rotate(vector);
    }
}
