using Zenject;

public sealed class LifeInstaller : Installer<int, int, LifeInstaller>
{    
    private readonly int _maxPoints;        
    private readonly int _hitPoints;    

    public LifeInstaller(int maxPoints, int hitPoints)
    {
        _maxPoints = maxPoints;
        _hitPoints = hitPoints;        
    }

    public override void InstallBindings()
    {        
        Container
            .BindInterfacesAndSelfTo<LifeComponent>()
            .AsSingle()
            .WithArguments(_maxPoints, _hitPoints);        
    }
}
