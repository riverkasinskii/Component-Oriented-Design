using UnityEngine;
using Zenject;

public sealed class LavaInstaller : MonoInstaller
{
    [Header("Collision Facade")]
    [SerializeField]
    private EntityCollisionFacade _entityCollisionFacade;

    public override void InstallBindings()
    {
        Container.Bind<EntityCollisionFacade>().FromInstance(_entityCollisionFacade).AsSingle();
        Container.BindInterfacesTo<DamageController>().AsCached().NonLazy();
    }
}
