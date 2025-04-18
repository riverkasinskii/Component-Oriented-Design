using System;
using UnityEngine;

public sealed class EntityCollisionFacade : MonoBehaviour
{
    [SerializeField] private int _damage;

    public Action<GameObject, Collider2D> OnInteractInvoked;
    public Action<GameObject, Collider2D, int> OnTakeDamaged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnTakeDamaged?.Invoke(gameObject, collision.collider, _damage);
        OnInteractInvoked?.Invoke(gameObject, collision.collider);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnTakeDamaged?.Invoke(gameObject, collision, _damage);
        OnInteractInvoked?.Invoke(gameObject, collision);        
    }
}
