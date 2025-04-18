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

    public void TryToss(IEntity entity)
    {
        if (_condition.IsTrue() && !IsCooldown)
        {
            Toss(entity);
        }
    }
        
    public void TryToss(List<Entity> targets, Transform transform)
    {
        var entity = TryGetInteractObject(targets, transform.position, _distanceToToss);
        if (_condition.IsTrue() && entity != null && !IsCooldown)
        {
            Toss(entity);
        }
    }

    private void Toss(IEntity entity)
    {
        var rb = entity.Get<Rigidbody2D>();
        Interact(rb, _forceToss, Vector2.up);
        OnTossed?.Invoke();
    }
}
