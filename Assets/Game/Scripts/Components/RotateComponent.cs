using System;
using UnityEngine;

public sealed class RotateComponent
{
    private readonly Transform _transform;             

    private readonly Condition _condition = new();

    public RotateComponent(Transform transform)
    {
        _transform = transform;
    }
            
    public void Rotate(Vector2 direction)
    {
        if (!_condition.IsTrue())
        {
            return;
        }                

        _transform.localScale = new Vector3(direction.x, 1, 1);
    }
        
    public void AddCondition(Func<bool> condition)
    {
        _condition.AddCondition(condition);
    }
}
