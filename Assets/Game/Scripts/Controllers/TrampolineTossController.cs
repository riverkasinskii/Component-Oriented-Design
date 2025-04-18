using System;
using UnityEngine;
using Zenject;

public sealed class TrampolineTossController : IInitializable, IDisposable
{   
    private readonly EntityCollisionFacade _facade;
    private readonly EntityProvider _entityProvider;

    private AudioComponent _audioComponent;
    private TossComponent _tossComponent;

    public TrampolineTossController(EntityCollisionFacade facade, EntityProvider entityProvider)
    {
        _facade = facade;
        _entityProvider = entityProvider;
    }

    void IInitializable.Initialize()
    {
        _audioComponent = _entityProvider.Value.Get<AudioComponent>();
        _tossComponent = _entityProvider.Value.Get<TossComponent>();

        _facade.OnInteractInvoked += InteractInvoked;        
        _tossComponent.OnTossed += OnTossed;
    }

    void IDisposable.Dispose()
    {
        _facade.OnInteractInvoked -= InteractInvoked;
        _tossComponent.OnTossed -= OnTossed;
    }

    private void OnTossed()
    {
        _audioComponent.PlayOneShot(AudioData.Trampline);
    }

    private void InteractInvoked(GameObject current, Collider2D target)
    {
        var tuple = InteractConditionHelper.TryGet(current, target);

        if (tuple.Item1 != null && tuple.Item2 != null)
        {
            TryToss(tuple.Item1, tuple.Item2);
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
