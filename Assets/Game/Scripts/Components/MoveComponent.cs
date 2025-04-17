using System;
using UnityEngine;
using Zenject;

public sealed class MoveComponent : ITickable
{
    public Action<Vector2> OnDirectionChanged;
    public Vector2 CurrentDirection { get; private set; } = Vector2.left;
    public Direction Direction { get; private set; } = Direction.Left;

    private readonly Transform _transform;
    private readonly Transform[] _wayPoints = null;
    private readonly float _speed;     
    private int i;

    private readonly Condition _condition = new();

    public MoveComponent(float moveSpeed, Transform transform)
    {
        _transform = transform;
        _speed = moveSpeed;        
    }

    public MoveComponent(float moveSpeed, Transform transform, Transform[] wayPoints)
    {
        _transform = transform;
        _speed = moveSpeed;
        _wayPoints = wayPoints;
    }
         
    public void MoveTranslate(Vector2 direction)
    {
        if (!_condition.IsTrue())
            return;
                
        CurrentDirection = direction;
        _transform.Translate(_speed * Time.deltaTime * direction);               
    }

    private void MoveTowards()
    {
        _transform.position = Vector2.MoveTowards(_transform.position, _wayPoints[i].position, Time.deltaTime * _speed);        
    }

    private Vector2 GetDirection(Vector2 current, Vector2 target)
    {
        float firstValue = MathF.Abs(current.x);
        float secondValue = MathF.Abs(target.x);
        float thirdValue = MathF.Abs(current.y);
        float fourthValue = MathF.Abs(target.y);

        if (firstValue > secondValue)
        {
            Direction = Direction.Right;
            return Vector2.right;
        }
        if (firstValue < secondValue)
        {
            Direction = Direction.Left;
            return Vector2.left;
        }
        if (thirdValue > fourthValue)
        {
            Direction = Direction.Up;
            return Vector2.up;
        }
        if (thirdValue < fourthValue)
        {
            Direction = Direction.Down;
            return Vector2.down;
        }
        return Vector2.zero;
    }

    public void AddCondition(Func<bool> condition)
    {
        _condition.AddCondition(condition);
    }

    void ITickable.Tick()
    {
        if (_transform != null && _wayPoints != null)
        {
            MoveTowards();           

            if (Vector2.Distance(_wayPoints[i].position, _transform.position) < 0.1f)
            {
                if (i > 0)
                {
                    i = 0;
                }
                else
                {
                    i = 1;
                }
                CurrentDirection = GetDirection(_wayPoints[i].position, _transform.position);
                OnDirectionChanged?.Invoke(CurrentDirection);
            }
        }
    }
}
