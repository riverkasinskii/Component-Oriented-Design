using UnityEngine;
using Zenject;

public sealed class TrapInstaller : MonoInstaller
{
    [Header("Life Component")]
    [SerializeField]
    private int _maxPoints;

    [SerializeField]
    private int _hitPoints;        

    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody2D _rb;

    [Header("Collision Facade")]
    [SerializeField]
    private EntityCollisionFacade _entityCollisionFacade;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();
        Container.Bind<EntityCollisionFacade>().FromInstance(_entityCollisionFacade).AsSingle();

        Container.Bind<Rigidbody2D>().FromInstance(_rb).AsSingle();
        LifeInstaller.Install(Container, _maxPoints, _hitPoints);

        if (TryGetComponent(out Entity entity))
        {
            Container.Bind<EntityProvider>().FromInstance(new EntityProvider(entity)).AsSingle();
            Container.BindInterfacesTo<EntityLifeController>().AsCached().NonLazy();
            Container.BindInterfacesTo<EntityDamageController>().AsCached().NonLazy();
        }
    }
}
