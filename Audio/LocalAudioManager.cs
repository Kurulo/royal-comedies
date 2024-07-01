using System.Collections.Generic;
using UnityEngine;

public class LocalAudioManager : MonoBehaviour
{
    [SerializeField] private List<AudioSource> _audioSources = new List<AudioSource>();

    public void PlaySound(AudioClip clip)
    {
        _audioSources[0].PlayOneShot(clip);
    }
}
