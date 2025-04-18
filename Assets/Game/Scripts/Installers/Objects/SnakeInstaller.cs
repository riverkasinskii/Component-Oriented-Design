using UnityEngine;
using Zenject;

public sealed class SnakeInstaller : MonoInstaller
{
    [Header("Life Component")]
    [SerializeField]
    private int _maxPoints;

    [SerializeField]
    private int _hitPoints;        

    [Header("Move Component")]
    [SerializeField]
    private float _moveSpeed = 2.5f;

    [SerializeField]
    private Transform[] _wayPoints;

    [SerializeField]
    private Transform _rootTransform;
        
    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody2D _rb;
        
    [Header("Toss Component")]
    [SerializeField]
    private int _forceToss = 20;

    [SerializeField]
    private float _distanceToToss = 3f;

    [SerializeField]
    private float _tossCooldown = 3f;

    [Header("Collision Facade")]
    [SerializeField]
    private EntityCollisionFacade _entityCollisionFacade;

    [Header("Audio Component")]
    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private AudioConfig _audioConfig;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
        Container.Bind<Rigidbody2D>().FromInstance(_rb).AsSingle();

        Container.BindInterfacesAndSelfTo<RotateComponent>().FromInstance(new RotateComponent(_rootTransform)).AsSingle();
        Container.Bind<AudioComponent>().FromInstance(new AudioComponent(_audioConfig, _audioSource)).AsSingle();
        MoveInstaller.Install(Container, _moveSpeed, _wayPoints, _rootTransform);
        LifeInstaller.Install(Container, _maxPoints, _hitPoints);        
        TossInstaller.Install(Container, _forceToss, _distanceToToss, _tossCooldown);        

        if (TryGetComponent(out Entity entity))
        {            
            Container.Bind<EntityProvider>().FromInstance(new EntityProvider(entity)).AsSingle();
            Container.BindInterfacesTo<EntityLifeController>().AsCached().NonLazy();
            Container.Bind<EntityCollisionFacade>().FromInstance(_entityCollisionFacade).AsSingle();
            Container.BindInterfacesAndSelfTo<EntityDamageController>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<EntityInteractListener>().AsCached().NonLazy();   
            Container.BindInterfacesTo<SnakeRotateController>().AsCached().NonLazy();
        }
    }
}
