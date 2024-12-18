using UnityEngine;

public class DoorSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public AudioClip keyClip;

    public void PlayDoorSound()
    {
        if (audioClips.Length == 0) return;
        int random = Random.Range(0, audioClips.Length);
        AudioClip randomCLip = audioClips[random];
        if (audioSource != null && randomCLip != null)
        {
            AudioManager.Instance.PlaySound(audioSource, randomCLip);
        }
    }

    public void PlayKeySound()
    {
        if(audioSource != null)
        {
            AudioManager.Instance.PlaySound(audioSource, keyClip);
        }
    }
}
