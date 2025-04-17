using UnityEngine;
using Zenject;

public sealed class CharacterRotateController : ITickable, IInitializable
{
    private readonly EntityProvider _character;
    private readonly IRotateInput _rotateInput;
    private RotateComponent _rotateComponent;

    public CharacterRotateController(EntityProvider character, IRotateInput rotateInput)
    {
        _character = character;
        _rotateInput = rotateInput;
    }

    void IInitializable.Initialize()
    {
        _rotateComponent = _character.Value.Get<RotateComponent>();
    }

    void ITickable.Tick()
    {
        Vector3 direction = _rotateInput.GetRotateDirection();
        _rotateComponent.Rotate(direction);
    }
}
