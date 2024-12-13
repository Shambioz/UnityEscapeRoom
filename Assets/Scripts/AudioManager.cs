using UnityEngine;
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioSource KeySource;
    public AudioSource FallSource;
    public AudioSource GlassesSource;
    public AudioSource PillsSource;
    public AudioSource DispenserSource;
    public AudioClip ItsTime;
    public AudioClip Beep;
    private float volume = 0.3f;

    public void Start()
    {
        PlaySound(DispenserSource, ItsTime);
    }
    public void PlayKeySound()
    {
        PlaySound(KeySource, Beep);
    }

    public void PlayFallSound()
    {
        PlaySound(FallSource, Beep);
    }

    public void PlayGlassesSound()
    {
        PlaySound(GlassesSource, Beep);
    }

    public void PlayPillsSound()
    {
        PlaySound(PillsSource, Beep);
    }
    public void PlaySound(AudioSource source, AudioClip clip)
    {
        source.PlayOneShot(clip, volume);
    }
}
