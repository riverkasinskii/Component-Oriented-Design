using UnityEngine;
using Zenject;

public sealed class SpiderInstaller : MonoInstaller
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

    [Header("Push Component")]
    [SerializeField]
    private int _forcePush = 10;

    [SerializeField]
    private float _distanceToPush = 4f;

    [SerializeField]
    private float _pushCooldown = 2f;

    [Header("Collision Facade")]
    [SerializeField]
    private EntityCollisionFacade _entityCollisionFacade;
        
    public override void InstallBindings()
    {
        Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
        Container.Bind<EntityCollisionFacade>().FromInstance(_entityCollisionFacade).AsSingle();

        MoveInstaller.Install(Container, _moveSpeed, _wayPoints, _rootTransform);
        LifeInstaller.Install(Container, _maxPoints, _hitPoints);
        PushInstaller.Install(Container, _forcePush, _distanceToPush, _pushCooldown);
        
        if (TryGetComponent(out Entity entity))
        {
            Container.Bind<Rigidbody2D>().FromInstance(_rb).AsSingle();
            Container.Bind<EntityProvider>().FromInstance(new EntityProvider(entity)).AsSingle();
            Container.BindInterfacesTo<EntityLifeController>().AsCached().NonLazy();            
            Container.BindInterfacesAndSelfTo<EntityDamageController>().AsCached().NonLazy();
            Container.BindInterfacesAndSelfTo<EntityInteractListener>().AsCached().NonLazy();
        }
    }
}
