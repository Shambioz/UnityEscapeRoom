using UnityEngine;

public class StartVoiceover : MonoBehaviour
{
    public AudioClip startClip;
    public AudioSource PlayerSource;
    private Collider trigger;

    private void Awake()
    {
        trigger = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySound(PlayerSource, startClip);
            trigger.enabled = false;
        }
    }
}
