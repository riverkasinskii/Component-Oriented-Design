using UnityEngine;
using Zenject;

public sealed class TrampolineInstaller : MonoInstaller
{
    [Header("Toss Component")]
    [SerializeField]
    private int _forceToss = 20;

    [SerializeField]
    private float _distanceToToss = 3f;

    [SerializeField]
    private float _cooldown = 3f;

    [Header("Audio Component")]
    [SerializeField]
    private AudioConfig _audioConfig;

    [SerializeField]
    private AudioSource _audioSource;

    [Header("Collision Facade")]
    [SerializeField]
    private EntityCollisionFacade _entityCollisionFacade;

    public override void InstallBindings()
    {        
        TossInstaller.Install(Container, _forceToss, _distanceToToss, _cooldown);
        Container.Bind<AudioComponent>().FromInstance(new AudioComponent(_audioConfig, _audioSource)).AsSingle();

        if (TryGetComponent(out Entity entity))
        {
            Container.Bind<EntityCollisionFacade>().FromInstance(_entityCollisionFacade).AsSingle();
            Container.Bind<EntityProvider>().FromInstance(new EntityProvider(entity)).AsSingle();            
            Container.BindInterfacesTo<TrampolineTossController>().AsCached().NonLazy();
        }
    }
}
