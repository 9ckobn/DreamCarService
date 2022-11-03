using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    private AudioSource audioSource;

    [SerializeField] private AudioClip[] audioClips;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void Step()
    {
        audioSource.PlayOneShot(audioClips[Random.Range(0, audioClips.Length)]);
    }
}
