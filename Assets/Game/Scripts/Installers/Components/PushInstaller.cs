using Zenject;

public sealed class PushInstaller : Installer<int, float, float, PushInstaller>
{        
    private readonly int _forcePush;        
    private readonly float _distanceToPush;
    private readonly float _pushCooldown;

    public PushInstaller(int forcePush, float distanceToPush, float pushCooldown)
    {
        _forcePush = forcePush;
        _distanceToPush = distanceToPush;       
        _pushCooldown = pushCooldown;
    }

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<PushComponent>()
            .FromInstance(new PushComponent(_forcePush, _distanceToPush, _pushCooldown))
            .AsSingle();
    }
}
