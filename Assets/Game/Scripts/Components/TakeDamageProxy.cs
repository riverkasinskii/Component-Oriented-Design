using UnityEngine;

public sealed class TakeDamageProxy : MonoBehaviour, IDamageable
{
    [SerializeField] private Entity entity;

    public void TakeDamage(int damage)
    {
        if(entity.TryGet(out LifeComponent lifeComponent))
        {
            lifeComponent.TakeDamage(damage);
        }
    }
}
