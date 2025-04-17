using System;
using UnityEngine;
using Zenject;

public sealed class JumpInput : IJumpInput, ITickable
{
    public event Action<Vector2> OnInputInvoked;

    private readonly InputMap _inputMap;    

    public JumpInput(InputMap inputMap)
    {
        _inputMap = inputMap;
    }       
    
    void ITickable.Tick()
    {
        if (Input.GetKeyDown(_inputMap.Jump))
            OnInputInvoked?.Invoke(Vector2.up);
    }
}
