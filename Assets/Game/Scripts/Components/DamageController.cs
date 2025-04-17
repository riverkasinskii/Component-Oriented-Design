using System;
using UnityEngine;
using Zenject;

public sealed class DamageController : IInitializable, IDisposable
{    
    private readonly EntityCollisionFacade _entityCollisionFacade;    
    
    public DamageController(EntityCollisionFacade entityCollisionFacade)
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

    private void TakeDamage(Collider2D collision, int damage)
    {        
        if (collision.gameObject.TryGetComponent(out IEntity target) && target.TryGet(out LifeComponent lifeComponent))
        {
            lifeComponent.TakeDamage(damage);
        }
        if (collision.gameObject.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(damage);
        }
    }
}