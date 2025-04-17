using UnityEngine;
using Zenject;

public sealed class MovingLavaInstaller : MonoInstaller
{
    [Header("Movement")]
    [SerializeField]
    private float _moveSpeed = 2.5f;

    [SerializeField]
    private Transform[] _wayPoints;

    [SerializeField]
    private Transform _rootTransform;

    public override void InstallBindings()
    {
        MoveInstaller.Install(Container, _moveSpeed, _wayPoints, _rootTransform);
    }
}
