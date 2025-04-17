using System;
using UnityEngine;

public sealed class EntityCollisionFacade : MonoBehaviour
{
    [SerializeField] private int _damage;

    public Action<Collider2D> OnInteractInvoked;
    public Action<Collider2D, int> OnTakeDamaged;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnInteractInvoked?.Invoke(collision.collider);
        OnTakeDamaged?.Invoke(collision.collider, _damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInteractInvoked?.Invoke(collision);
        OnTakeDamaged?.Invoke(collision, _damage);
    }
}
