using UnityEngine;

[CreateAssetMenu(
        fileName = "InputMap",
        menuName = "SampleGame/New InputMap"
    )]
public sealed class InputMap : ScriptableObject
{
    [field: SerializeField]
    public KeyCode MoveLeft { get; private set; } = KeyCode.LeftArrow;

    [field: SerializeField]
    public KeyCode MoveRight { get; private set; } = KeyCode.RightArrow;
            
    [field: SerializeField]
    public KeyCode Jump { get; private set; } = KeyCode.Space;

    [field: SerializeField]
    public KeyCode Push { get; private set; } = KeyCode.Mouse0;

    [field: SerializeField]
    public KeyCode Toss { get; private set; } = KeyCode.Mouse1;
}
