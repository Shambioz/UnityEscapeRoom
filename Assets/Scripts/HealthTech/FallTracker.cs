using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FallTracker : MonoBehaviour
{
    private XRGrabInteractable XRGrabInteractable;
    public SnapZone SnapZone;

    private void Awake()
    {
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        XRGrabInteractable.selectEntered.AddListener(OnGrabbed);
    }

    private void OnGrabbed(SelectEnterEventArgs e)
    {
        if(SnapZone !=  null)
        {
            SnapZone.Highlight(true);
        }
    }
}
