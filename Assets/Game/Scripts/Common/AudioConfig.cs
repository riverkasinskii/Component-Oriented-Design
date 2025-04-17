using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "AudioConfigs", order = 1)]
public sealed class AudioConfig : ScriptableObject
{
    public AudioClip takeDamageCharacterClip;
    public AudioClip takeDamageEnemyClip;
    public AudioClip TossClip;
    public AudioClip PushClip;
    public AudioClip LavaClip;
    public AudioClip JumpClip;
    public AudioClip TramplineClip;

    public AudioClip GetAudioClip(AudioData data)
    {
        return data switch
        {
            AudioData.Push => PushClip,
            AudioData.Toss => TossClip,
            AudioData.Jump => JumpClip,
            AudioData.TakeDamageEnemy => takeDamageEnemyClip,
            AudioData.TakeDamageCharacter => takeDamageCharacterClip,
            AudioData.Lava => LavaClip,
            AudioData.Trampline => TramplineClip,
            _ => null,
        };
    }
}

public enum AudioData
{
    Push,
    Toss,
    Jump,
    TakeDamageEnemy,
    TakeDamageCharacter,
    Lava,
    Trampline
}
