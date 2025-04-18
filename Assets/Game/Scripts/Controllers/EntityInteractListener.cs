using System;
using UnityEngine;
using Zenject;

public sealed class EntityInteractListener : IInitializable, IDisposable
{    
    private readonly EntityDamageController _damageController;        

    public EntityInteractListener(EntityDamageController damageController)
    {
        _damageController = damageController;        
    }

    void IInitializable.Initialize()
    {                
        _damageController.OnDamaged += OnDamaged;
    }

    void IDisposable.Dispose()
    {                
        _damageController.OnDamaged -= OnDamaged;
    }

    private void OnDamaged(GameObject currentEntity, Collider2D target)
    {
        var tuple = InteractConditionHelper.TryGet(currentEntity, target);
        if (tuple.Item1 != null && tuple.Item2 != null)
        {
            TryPush(tuple.Item1, tuple.Item2);
            TryToss(tuple.Item1, tuple.Item2);
        }                
    }           

    private void TryPush(IEntity currentEntity, IEntity target)
    {
        if (currentEntity.TryGet(out PushComponent pushComponent))
        {            
            var moveComponent = currentEntity.Get<MoveComponent>();
            Vector2 currentDirection = moveComponent.CurrentDirection;
            pushComponent.Push(target, currentDirection);            
        }
    }

    private void TryToss(IEntity currentEntity, IEntity target)
    {
        if (currentEntity.TryGet(out TossComponent tossComponent))
        {            
            tossComponent.TryToss(target);
        }
    }
}
