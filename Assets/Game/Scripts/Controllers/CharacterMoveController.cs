using System;
using UnityEngine;
using Zenject;

public sealed class CharacterMoveController : IInitializable, IDisposable
{
    private readonly EntityProvider _character;
    private readonly IMoveInput _moveInput;
    private MoveComponent _moveComponent;

    public CharacterMoveController(EntityProvider character, IMoveInput moveInput)
    {
        _character = character;
        _moveInput = moveInput;
    }

    void IInitializable.Initialize()
    {
        _moveComponent = _character.Value.Get<MoveComponent>();

        _moveInput.OnInputInvoked += InputListener;
    }

    void IDisposable.Dispose()
    {
        _moveInput.OnInputInvoked -= InputListener;
    }

    private void InputListener(Vector2 direction)
    {
        _moveComponent.MoveTranslate(direction);
    }
}
