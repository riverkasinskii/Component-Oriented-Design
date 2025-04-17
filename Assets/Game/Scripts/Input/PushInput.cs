using System;
using UnityEngine;
using Zenject;

public sealed class PushInput : IPushInput, ITickable
{
    public event Action OnInputInvoked;

    private readonly InputMap _inputMap;

    public PushInput(InputMap inputMap)
    {
        _inputMap = inputMap;
    }

    void ITickable.Tick()
    {
        if (Input.GetKeyDown(_inputMap.Push))
            OnInputInvoked?.Invoke();
    }
}
