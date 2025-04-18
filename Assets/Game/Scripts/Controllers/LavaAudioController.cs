using System;
using UnityEngine;
using Zenject;

public sealed class LavaAudioController : IInitializable, IDisposable
{        
    private readonly EntityDamageController _damageController;    

    public LavaAudioController(EntityDamageController damageController)
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

    private void OnDamaged(GameObject currentValue, Collider2D targetValue)
    {
        if(currentValue.TryGetComponent(out AudioSource audioSource))
        {
            audioSource.PlayOneShot(audioSource.clip);
        }
    }
}
