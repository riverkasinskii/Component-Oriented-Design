using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractComponent
{    
    protected readonly Condition _condition = new();

    protected bool IsCooldown { get; private set; } = false;
               
    protected IEntity TryGetInteractObject(List<Entity> targets, Vector2 currentPosition, float distance)
    {
        foreach (var target in targets)
        {
            Vector2 position = target.Get<Rigidbody2D>().position;
            if (Vector2.Distance(currentPosition, position) <= distance)
            {
                return target;
            }            
        }   
        return null;
    }

    protected void Interact(Rigidbody2D rb, int forcePush, Vector2 direction)
    {
        rb.AddForce(direction * forcePush, ForceMode2D.Impulse);
    }

    public void AddCondition(Func<bool> condition)
    {
        _condition.AddCondition(condition);
    }        
}
