using System;
using System.Collections;
using System.Xml.Serialization;
using UnityEngine;

public class Voiceovers : MonoBehaviour
{
    public AudioSource PlayerSource;
    public AudioSource GrandpaSource;
    public AudioClip[] oldManAudioClips;
    public AudioClip[] playerAudioClips;
    public AudioClip startClip;
    private Collider trigger;
    private float interval = 20f;
    private Coroutine timer;


    void Start()
    {
        trigger = GetComponent<BoxCollider>();
        timer = StartCoroutine(Timer());
    }

    private void RegularPhrase()
    {
        int random = UnityEngine.Random.Range(0, 6);
        AudioManager.Instance.PlaySound(GrandpaSource, oldManAudioClips[random]);
    }

    private IEnumerator Timer()
    {
        while (true)
        {
            RegularPhrase();
            yield return new WaitForSeconds(interval);
        }
    }
}
