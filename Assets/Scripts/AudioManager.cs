using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio clip and sources")]
    public AudioClip RemoteSound;
    public AudioSource KeySource;
    public AudioSource GlassesSource;
    public AudioSource FallTrackerSource;
    public AudioSource PillsSource;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        KeySource = GetComponent<AudioSource>();
        GlassesSource = GetComponent<AudioSource>();
        FallTrackerSource = GetComponent<AudioSource>();
        PillsSource = GetComponent<AudioSource>();
        EventManager.Instance.StartListening(SoundEventType.Keys, OnKeyClicked);
        EventManager.Instance.StartListening(SoundEventType.Glasses, OnGlassesClicked);
        EventManager.Instance.StartListening(SoundEventType.FallTracker, OnFallTrackerClicked);
        EventManager.Instance.StartListening(SoundEventType.Pills, OnPillsClicked);
    }

    public void OnKeyClicked()
    {
        PlaySoundEffect(KeySource, RemoteSound);
    }

    public void OnGlassesClicked()
    {
        PlaySoundEffect(GlassesSource, RemoteSound);
    }

    public void OnFallTrackerClicked()
    {
        PlaySoundEffect(FallTrackerSource, RemoteSound);
    }

    public void OnPillsClicked()
    {
        PlaySoundEffect(PillsSource, RemoteSound);
    }
    
    private void PlaySoundEffect(AudioSource source, AudioClip clip)
    {
        if (clip == null) return;
        source.PlayOneShot(clip);
    }

    private void OnDestroy()
    {
        if(EventManager.Instance != null)
        {
            EventManager.Instance.StopListening(SoundEventType.Keys, OnKeyClicked);
            EventManager.Instance.StopListening(SoundEventType.Glasses, OnGlassesClicked);
            EventManager.Instance.StopListening(SoundEventType.FallTracker, OnFallTrackerClicked);
            EventManager.Instance.StopListening(SoundEventType.Pills, OnPillsClicked);
        }
    }
}
