using UnityEngine;

public sealed class CollisionGroundListener : MonoBehaviour
{        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        TrySetGroundState(collision, true);
    }
        
    private void OnCollisionExit2D(Collision2D collision)
    {
        TrySetGroundState(collision, false);
    }

    private static void TrySetGroundState(Collision2D collision, bool state)
    {
        if (collision.gameObject.TryGetComponent(out IEntity entity) && entity.TryGet(out GroundComponent groundComponent))
        {
            groundComponent.SetGroundState(state);
        }
    }
}
