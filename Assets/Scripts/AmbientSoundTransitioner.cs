using System;
using UnityEngine;

public class AmbientSoundTransitioner : MonoBehaviour
{
    [SerializeField] private AudioSource ambientAudioSource;
    [SerializeField, Range(0, 1)] private float interiorVolume = 0.2f;
    [SerializeField, Range(0, 1)] private float exteriorVolume = 1f;

    private bool isOutside = true;
    private void OnTriggerEnter(Collider other)
    {
        if (isOutside)
        {
            isOutside = false;
            ambientAudioSource.volume = interiorVolume;
        }
        else
        {
            isOutside = true;
            ambientAudioSource.volume = exteriorVolume;
        }
    }
}
