using System;
using System.Collections.Generic;
using UnityEngine;

public sealed class TossComponent : InteractComponent
{
    public float TossCooldown {  get; private set; }

    public Action OnTossed;
    public Action<GameObject> OnRootGameObject;

    private readonly int _forceToss;
    private readonly float _distanceToToss;    

    public TossComponent(int forceToss, float distanceToToss, float tossCooldown)
    {
        _forceToss = forceToss;
        _distanceToToss = distanceToToss;        
        TossCooldown = tossCooldown;
    }

    public void Toss(IEntity entity)
    {
        if (_condition.IsTrue() && !IsCooldown)
        {
            var rb = entity.Get<Rigidbody2D>();
            Interact(rb, _forceToss, Vector2.up);            
        }
    }

    public void Toss(List<Entity> targets, Transform transform, Vector2 direction)
    {
        var target = TryGetInteractObject(targets, transform.position, _distanceToToss);
        if (_condition.IsTrue() && target != null && !IsCooldown)
        {
            var rb = target.Get<Rigidbody2D>();
            Interact(rb, _forceToss, direction);                        
            OnTossed?.Invoke();
        }
    }  
}
