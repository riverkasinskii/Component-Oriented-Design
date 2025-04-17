using UnityEngine;
using Zenject;

public sealed class CameraFollower : ILateTickable
{
    private readonly EntityProvider character;
    private readonly Camera camera;
    private readonly Vector3 _cameraOffset;

    public CameraFollower(EntityProvider character, Camera camera, Vector3 cameraOffset)
    {
        this.character = character;
        this.camera = camera;
        _cameraOffset = cameraOffset;
    }

    void ILateTickable.LateTick()
    {
        IEntity character = this.character.Value;        
        Vector3 cameraPosition = character.Get<Transform>().position + _cameraOffset;
        this.camera.transform.position = cameraPosition;
    }
}
