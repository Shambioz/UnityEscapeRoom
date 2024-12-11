using System;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEventType
{
    Keys,
    Glasses,
    FallTracker,
    Pills
}
public class EventManager : MonoBehaviour
{
    public static EventManager Instance {  get; private set; }
    private Dictionary<SoundEventType, Action> eventDictionary;
    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            eventDictionary = new Dictionary<SoundEventType, Action>();
            DontDestroyOnLoad(gameObject);
        }
    }

    public void StartListening(SoundEventType eventType, Action listener)
    {
        if (!eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] = listener;
        }
        else
        {
            eventDictionary[eventType] += listener;
        }
    }

    public void StopListening(SoundEventType eventType, Action listener)
    {
        if (eventDictionary.ContainsKey(eventType))
        {
            eventDictionary[eventType] -= listener;
        }
    }

    public void TriggerEvent(SoundEventType eventType)
    {
        if(eventDictionary.ContainsKey(eventType) && eventDictionary[eventType] != null)
        {
            eventDictionary[eventType].Invoke();
        }
    }
}
