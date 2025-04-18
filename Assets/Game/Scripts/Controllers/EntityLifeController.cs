using System;
using UnityEngine;
using Zenject;

public sealed class EntityLifeController : IInitializable, IDisposable
{
    private readonly EntityProvider _entityProvider;
    private GameObject _gameObject;
    private LifeComponent _lifeComponent;    

    public EntityLifeController(EntityProvider entityProvider)
    {        
        _entityProvider = entityProvider;        
    }
    void IInitializable.Initialize()
    {
        _lifeComponent = _entityProvider.Value.Get<LifeComponent>();        
        _gameObject = _entityProvider.Value.Get<GameObject>();        
        _lifeComponent.OnEmpty += LifeEmpty;
        _lifeComponent.OnLifeChanged += LifeChanged;
        
    }

    void IDisposable.Dispose()
    {
        _lifeComponent.OnEmpty -= LifeEmpty;
        _lifeComponent.OnLifeChanged -= LifeChanged;
    }

    private void LifeChanged()
    {        
        if (_entityProvider.Value.TryGet(out AudioComponent audioComponent))
        {
            audioComponent.PlayOneShot(AudioData.TakeDamageEnemy);
        }        
    }

    private void LifeEmpty()
    {
        _gameObject.SetActive(false);
    }
}
