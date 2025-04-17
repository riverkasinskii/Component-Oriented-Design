using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class PushComponent : InteractComponent
{
    public float PushCooldown {  get; private set; }

    public event Action OnPushed;
        
    private readonly int _forcePush;
    private readonly float _distanceToPush;    

    public PushComponent(int forcePush, float distanceToPush, float pushCooldown)
    {
        _forcePush = forcePush;
        _distanceToPush = distanceToPush;       
        PushCooldown = pushCooldown;
    }
    
    public void Push(IEntity entity, Vector2 currentDirection)
    {        
        if (_condition.IsTrue() && !IsCooldown)
        {
            var rb = entity.Get<Rigidbody2D>();                        
            Interact(rb, _forcePush, currentDirection);            
        }
    }

    public void Push(List<Entity> targets, Transform transform, Vector2 currentDirection)
    {
        var target = TryGetInteractObject(targets, transform.position, _distanceToPush);
        if (_condition.IsTrue() && target != null && !IsCooldown)
        {
            var rb = target.Get<Rigidbody2D>();

            if (transform.localScale.x > 0)
            {
                Interact(rb, _forcePush, currentDirection);
            }
            if (transform.localScale.x < 0)
            {
                Interact(rb, _forcePush, currentDirection);
            }

            OnPushed?.Invoke();            
        }
    }
}
