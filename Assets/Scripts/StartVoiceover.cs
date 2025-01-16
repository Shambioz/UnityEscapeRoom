using System.Collections;
using UnityEngine;

public class StartVoiceover : MonoBehaviour
{
    public AudioClip startClip;
    public AudioSource PlayerSource;
    public CharacterController movement;
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
            movement.enabled = false;
            trigger.enabled = false;
            StartCoroutine(WaitForClip());
        }
    }

    private IEnumerator WaitForClip()
    {
        yield return new WaitForSeconds(startClip.length);
        movement.enabled = true;
    }
}
