using UnityEngine;

public sealed class AudioComponent
{
    private readonly AudioConfig _audioConfig;
    private readonly AudioSource _audioSource;

    public AudioComponent(AudioConfig audioConfig, AudioSource audioSource)
    {
        _audioConfig = audioConfig;
        _audioSource = audioSource;
    }
        
    public void PlayOneShot(AudioData data)
    {
        AudioClip clip = _audioConfig.GetAudioClip(data);
        _audioSource.PlayOneShot(clip);
    }

    public void Play() 
        => _audioSource.Play();
}


