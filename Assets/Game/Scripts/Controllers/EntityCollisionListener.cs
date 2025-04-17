using System;
using UnityEngine;
using Zenject;

public sealed class EntityCollisionListener : IInitializable, IDisposable
{
    private readonly EntityCollisionFacade _facade;    
    public EntityCollisionListener(EntityCollisionFacade facade)
    {
        _facade = facade;        
    }

    void IInitializable.Initialize()
    {
        _facade.OnInteractInvoked += InteractInvoker;
    }

    void IDisposable.Dispose()
    {
        _facade.OnInteractInvoked -= InteractInvoker;
    }

    private void InteractInvoker(Collider2D collider)
    {
        TryInteract(collider);
        TryInteractInParent(collider);
    }

    private void TryInteract(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEntity target))
        {
            Interact(collision, target);
        }
    }

    private void TryInteractInParent(Collider2D collision)
    {
        if (TryGetComponentInParent(collision, out IEntity target))
        {
            Interact(collision, target);
        }
    }

    private void Interact(Collider2D collision, IEntity target)
    {
        if (TryGetComponentInParent(collision, out IEntity currentEntity))
        {
            TryPush(currentEntity, target);
            TryToss(currentEntity, target);
        }
    }

    private bool TryGetComponentInParent(Collider2D collision, out IEntity entity)
    {
        entity = collision.gameObject.GetComponentInParent<IEntity>();
        return entity != null;
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
            if (currentEntity.TryGet(out TimerComponent timer) && !timer.IsActive)
            {
                timer.Start(tossComponent.TossCooldown);
                tossComponent.Toss(target);
            }            
        }
    }
}
