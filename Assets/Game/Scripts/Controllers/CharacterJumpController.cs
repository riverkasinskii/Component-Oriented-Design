using System;
using UnityEngine;
using Zenject;

public sealed class CharacterJumpController : IInitializable, IDisposable
{
    private readonly EntityProvider _character;
    private readonly IJumpInput _jumpInput;
    private JumpComponent _jumpComponent;
    private AudioComponent _audioComponent;

    private Rigidbody2D _rb;    

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
    }

    void IDisposable.Dispose()
    {
        _jumpInput.OnInputInvoked -= OnInputListener;
        _jumpComponent.OnJumped -= Jumped;
    }

    private void Jumped()
    {
        _audioComponent.PlayOneShot();
    }

    private void OnInputListener(Vector2 vector)
    {        
        _jumpComponent.Jump(vector, _rb);
    }
}
