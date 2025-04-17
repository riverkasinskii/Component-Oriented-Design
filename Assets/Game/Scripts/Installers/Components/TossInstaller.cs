using Zenject;

public sealed class TossInstaller : Installer<int, float, float, TossInstaller>
{
    private readonly int _forceToss;
    private readonly float _distanceToToss;
    private readonly float _tossCooldown;

    public TossInstaller(int forceToss, float distanceToToss, float tossCooldown)
    {
        _forceToss = forceToss;
        _distanceToToss = distanceToToss;      
        _tossCooldown = tossCooldown;
    }

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<TossComponent>()
            .FromInstance(new TossComponent(_forceToss, _distanceToToss, _tossCooldown))
            .AsSingle();
    }
}
