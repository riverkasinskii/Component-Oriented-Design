using Zenject;

public sealed class InputInstaller : Installer<InputMap, InputInstaller>
{    
    private readonly InputMap _inputMap;

    public InputInstaller(InputMap inputMap)
    {
        _inputMap = inputMap;
    }

    public override void InstallBindings()
    {
        this.Container.Bind<InputMap>().FromInstance(_inputMap).AsSingle();        
        this.Container.BindInterfacesTo<MoveInput>().AsSingle();
        this.Container.BindInterfacesTo<RotateInput>().AsSingle();
        this.Container.BindInterfacesTo<JumpInput>().AsSingle();
        this.Container.BindInterfacesTo<PushInput>().AsSingle();
        this.Container.BindInterfacesTo<TossInput>().AsSingle();
    }
}
