using System;
using UnityEngine;
using Zenject;

public sealed class TossInput : ITossInput, ITickable
{
    public event Action OnInputInvoked;

    private readonly InputMap _inputMap;

    public TossInput(InputMap inputMap)
    {
        _inputMap = inputMap;
    }

    void ITickable.Tick()
    {
        if (Input.GetKeyDown(_inputMap.Toss))
            OnInputInvoked?.Invoke();
    }
}
