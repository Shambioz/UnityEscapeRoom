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

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Start()
    {
        
    }

    public void PlayKeySound()
    {
        PlaySound(KeySource, Beep);
    }

    public void PlayFallSound()
    {
        Debug.Log("Fall");
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
        Debug.Log(source.name);
        source.Stop();
        source.PlayOneShot(clip, volume);
    }
}
