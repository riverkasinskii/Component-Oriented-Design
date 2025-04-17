using UnityEngine;

public sealed class RotateInput : IRotateInput
{
    private readonly InputMap _inputMap;
    private Vector2 _direction = Vector2.one;

    public RotateInput(InputMap inputMap)
    {
        _inputMap = inputMap;
    }

    public Vector2 GetRotateDirection()
    {        
        if (Input.GetKey(_inputMap.MoveLeft))
            _direction.x = -1;
        else if (Input.GetKey(_inputMap.MoveRight))
            _direction.x = 1;

        return _direction;
    }
}
