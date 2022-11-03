using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class VFXManager : MonoBehaviour
{
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    [SerializeField] private AudioClip MoneyPlump;
    [SerializeField] private AudioClip MoneyWhoosh;

    [SerializeField] private AudioClip ItemWhoosh;

    [SerializeField] private AudioClip CarRepair;

    public void MoneyEarn() => audioSource.PlayOneShot(MoneyPlump);

    public void MoneyGenerate() => audioSource.PlayOneShot(MoneyWhoosh);

    public void ItemsWhoosh() => audioSource.PlayOneShot(ItemWhoosh);

    public void CarsRepair() => audioSource.PlayOneShot(CarRepair);
}
