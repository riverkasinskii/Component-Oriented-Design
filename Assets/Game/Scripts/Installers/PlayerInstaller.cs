using UnityEngine;
using Zenject;

public sealed class PlayerInstaller : MonoInstaller
{
    [SerializeField]
    private Entity _characterPrefab;

    [SerializeField]
    private InputMap _inputMap;

    [SerializeField]
    private Transform _transform;

    [SerializeField]
    private Vector3 _cameraOffset;

    [SerializeField]
    private Camera _cameraPrefab;

    public override void InstallBindings()
    {
        CameraInstaller.Install(this.Container, _cameraPrefab, _cameraOffset);
        CharacterSystemInstaller.Install(this.Container, _characterPrefab, _transform);
        InputInstaller.Install(this.Container, _inputMap);                
    }
}
