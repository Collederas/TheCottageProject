using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundTrigger : MonoBehaviour
{
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    private bool playing;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playing = !playing;
            audioSource.Play();
            audioSource.volume = playing ? 1f : 0f;
        }
    }
}
