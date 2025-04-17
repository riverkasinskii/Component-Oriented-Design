using UnityEngine;
using Zenject;

public sealed class MoveInstaller : Installer<float, Transform[], Transform, MoveInstaller>
{
    private readonly float _moveSpeed;
    private readonly Transform[] _wayPoints;    
    private readonly Transform _rootTransform;

    public MoveInstaller(float moveSpeed, Transform[] wayPoints, Transform rootTransform)
    {
        _moveSpeed = moveSpeed;
        _wayPoints = wayPoints;
        _rootTransform = rootTransform;
    }    

    public override void InstallBindings()
    {
        Container
            .BindInterfacesAndSelfTo<MoveComponent>()
            .FromInstance(new MoveComponent(_moveSpeed, _rootTransform, _wayPoints))
            .AsSingle();            
    }
}
