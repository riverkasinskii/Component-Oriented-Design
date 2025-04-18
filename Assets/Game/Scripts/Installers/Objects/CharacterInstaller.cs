using System;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Zenject;

public sealed class CharacterInstaller : MonoInstaller
{
    [Header ("Life Component")]
    [SerializeField]
    private int _maxPoints;

    [SerializeField]
    private int _hitPoints;
        
    [Header("Move Component")]
    [SerializeField]
    private float _moveSpeed = 2f;

    [Header("Jump Component")]
    [SerializeField]
    private float jumpForce = 8f;

    [SerializeField]
    private float coolDown = 4f;

    [Header("Push Component")]
    [SerializeField]
    private int _forcePush = 10;

    [SerializeField]
    private float _distanceToPush = 4f;

    [SerializeField]
    private float _pushCooldown = 2f;

    [Header("Toss Component")]
    [SerializeField]
    private int _forceToss = 20;

    [SerializeField]
    private float _distanceToToss = 3f;

    [SerializeField]
    private float _tossCooldown = 3f;

    [Header("Audio Component")]
    [SerializeField]
    private AudioConfig _audioConfig;

    [SerializeField]
    private AudioSource _audioSource;

    public override void InstallBindings()
    {
        Container.Bind<GameObject>().FromInstance(gameObject).AsSingle();

        LifeInstaller.Install(Container, _maxPoints, _hitPoints);
        MoveInstaller.Install(Container, _moveSpeed, null, gameObject.transform);
        PushInstaller.Install(Container, _forcePush, _distanceToPush, _pushCooldown);
        TossInstaller.Install(Container, _forceToss, _distanceToToss, _tossCooldown);
                
        Container.Bind<RotateComponent>().AsSingle().WithArguments(gameObject.transform);

        Container.BindInterfacesAndSelfTo<JumpComponent>().AsSingle().WithArguments(jumpForce, coolDown);                        

        Container.BindInterfacesAndSelfTo<GroundComponent>().AsSingle();

        Container.Bind<AudioComponent>().FromInstance(new AudioComponent(_audioConfig, _audioSource)).AsSingle();
    }
}
