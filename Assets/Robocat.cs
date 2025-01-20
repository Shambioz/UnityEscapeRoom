using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class Robocat : MonoBehaviour
{
    [SerializeField] private GameObject Key;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private AudioSource AudioSource;
    [SerializeField] private AudioClip audioClipGiveKey;
    [SerializeField] private AudioClip audioClipCompleteTasks;
    private XRGrabInteractable XRGrabInteractable;
    private bool isDone = false;

    private void Awake()
    {
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
        XRGrabInteractable.selectEntered.AddListener(SpawnKey);
    }


    public void SpawnKey(SelectEnterEventArgs e)
    {
        if (TaskManager.Instance.AllTasksCompleted1() && !isDone)
        {
            AudioManager.Instance.PlaySound(AudioSource, audioClipGiveKey);
            Instantiate(Key, spawnPoint.position, spawnPoint.rotation);
            isDone = !isDone;
        }
        else
        {
            AudioManager.Instance.PlaySound(AudioSource, audioClipCompleteTasks);
        }
    }
}
