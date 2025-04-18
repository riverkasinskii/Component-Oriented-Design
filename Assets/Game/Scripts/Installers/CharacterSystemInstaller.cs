using UnityEngine;
using Zenject;

public sealed class CharacterSystemInstaller : Installer<Entity, Transform, CharacterSystemInstaller>
{
    private readonly Entity _characterPrefab;

    private readonly Transform _parent;

    public CharacterSystemInstaller(Entity characterPrefab, Transform parent)
    {
        _characterPrefab = characterPrefab;
        _parent = parent;
    }
                   
    public override void InstallBindings()
    {
        Container
            .Bind<EntityProvider>()
            .FromMethod(ctx =>
            {
                Transform parent = _parent;
                DiContainer diContainer = ctx.Container;
                Entity character = diContainer.InstantiatePrefabForComponent<Entity>(_characterPrefab, parent);                
                return new EntityProvider(character);
            })
            .AsSingle();
               
        Container.BindInterfacesTo<CharacterMoveController>().AsCached().NonLazy();

        Container.BindInterfacesTo<CharacterRotateController>().AsCached().NonLazy();

        Container.BindInterfacesTo<CharacterJumpController>().AsCached().NonLazy();

        Container.BindInterfacesTo<CharacterConditionObserver>().AsCached().NonLazy();

        Container.BindInterfacesTo<CharacterPushController>().AsCached().NonLazy();

        Container.BindInterfacesTo<CharacterTossController>().AsCached().NonLazy();
               
        Container.BindInterfacesTo<EntityLifeController>().AsCached().NonLazy();
                
        Container.BindInterfacesAndSelfTo<EnemyManager>().
            FromInstance(new EnemyManager())
            .AsSingle()
            .OnInstantiated<EnemyManager>((context, target) => {
                var entities = context.Container.ResolveAll<Entity>();
                target.SetEnemies(entities);
            });

    }
}
