using UnityEngine;

public class RemoteFunctions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void OnButtonClick(SoundEventType eventType)
    {
        EventManager.Instance.TriggerEvent(eventType);
    }
}
