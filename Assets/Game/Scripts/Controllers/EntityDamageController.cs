using System;
using UnityEngine;
using Zenject;

public sealed class EntityDamageController : IInitializable, IDisposable
{
    public event Action<GameObject, Collider2D> OnDamaged;

    private readonly EntityCollisionFacade _entityCollisionFacade;    
    
    public EntityDamageController(EntityCollisionFacade entityCollisionFacade)
    {        
        _entityCollisionFacade = entityCollisionFacade;       
    }
        
    void IInitializable.Initialize()
    {        
        _entityCollisionFacade.OnTakeDamaged += TakeDamage;                
    }

    void IDisposable.Dispose()
    {
        _entityCollisionFacade.OnTakeDamaged -= TakeDamage;
    }

    private void TakeDamage(GameObject currentEntity, Collider2D collision, int damage)
    {        
        if (collision.gameObject.TryGetComponent(out IEntity target) && target.TryGet(out LifeComponent lifeComponent))
        {
            lifeComponent.TakeDamage(damage);
            OnDamaged?.Invoke(currentEntity, collision);
        }
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
            OnDamaged?.Invoke(currentEntity, collision);
        }
    }
}