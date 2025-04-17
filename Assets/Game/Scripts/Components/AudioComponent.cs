using UnityEngine;

public sealed class AudioComponent
{
    private readonly AudioClip _audioClip;
    private readonly AudioSource _audioSource;

    public AudioComponent(AudioClip audioClip, AudioSource audioSource)
    {
        _audioClip = audioClip;
        _audioSource = audioSource;
    }

    public void PlayOneShot() 
        => _audioSource.PlayOneShot(_audioClip);

    public void Play() 
        => _audioSource.Play();
}


