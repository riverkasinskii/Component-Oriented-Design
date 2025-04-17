using UnityEngine;
using Zenject;

public sealed class CameraInstaller : Installer<Camera, Vector3, CameraInstaller>
{
    [Inject]
    private readonly Camera _cameraPrefab;

    [Inject]
    private readonly Vector3 _cameraOffset;

    public override void InstallBindings()
    {
        this.Container
            .Bind<Camera>()
            .FromComponentInNewPrefab(_cameraPrefab)
            .AsSingle();

        this.Container
            .BindInterfacesTo<CameraFollower>()
            .AsCached()
            .WithArguments(_cameraOffset);
    }
}
