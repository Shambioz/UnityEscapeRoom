using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ItsTime : MonoBehaviour
{
    private bool isDone = false;
    private XRGrabInteractable interactable;
    public AudioSource audioSource;
    public AudioClip clip;

    private void Start()
    {
        interactable = GetComponent<XRGrabInteractable>();
        interactable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs e)
    {
        if(!isDone)
        {
            isDone = true;
            AudioManager.Instance.PlaySound(audioSource, clip);
        }
    }
}
