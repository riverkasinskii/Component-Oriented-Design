using System;
using UnityEngine;
using Zenject;

public sealed class MoveInput : IMoveInput, ITickable
{
    public event Action<Vector2> OnInputInvoked;

    private readonly InputMap _inputMap;

    public MoveInput(InputMap inputMap)
    {        
        _inputMap = inputMap;
    }
        
    void ITickable.Tick()
    {
        if (Input.GetKey(_inputMap.MoveLeft))
            OnInputInvoked?.Invoke(Vector2.left);            
        else if (Input.GetKey(_inputMap.MoveRight))
            OnInputInvoked?.Invoke(Vector2.right);
    }
}
