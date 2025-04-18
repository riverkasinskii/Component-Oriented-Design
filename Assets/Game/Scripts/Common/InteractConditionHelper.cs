using UnityEngine;

public static class InteractConditionHelper
{
    public static (IEntity, IEntity) TryGet(GameObject current, Collider2D collider)
    {
        return TryGetInteract(current, collider);        
    }

    private static (IEntity, IEntity) TryGetInteract(GameObject current, Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out IEntity target))
        {
            return Interact(current, target);
        }
        else
        {
            return TryGetInteractInParent(current, collision);
        }
    }

    private static (IEntity, IEntity) TryGetInteractInParent(GameObject current, Collider2D collision)
    {
        if (TryGetComponentInParent(collision.gameObject, out IEntity target))
        {
            return Interact(current, target);
        }
        return (null, null);
    }

    private static (IEntity, IEntity) Interact(GameObject gameObject, IEntity target)
    {
        if (TryGetComponentInParent(gameObject, out IEntity currentEntity))
        {
            return (currentEntity, target);
        }
        return (null, null);
    }

    private static bool TryGetComponentInParent(GameObject gameObject, out IEntity entity)
    {
        entity = gameObject.GetComponentInParent<IEntity>();
        return entity != null;
    }
}
