using Zenject;

public sealed class EnemySystemInstaller : MonoInstaller
{        
    public override void InstallBindings()
    {     
        this.Container
            .Bind<EntityProvider>()
            .FromMethod(ctx =>
            {                
                DiContainer diContainer = ctx.Container;                
                Entity entity = diContainer.InjectGameObjectForComponent<Entity>(gameObject);
                return new EntityProvider(entity);
            })
            .AsSingle();         
    }
}
