using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FallTracker : MonoBehaviour
{
    private XRGrabInteractable XRGrabInteractable;
    private SnapZone SnapZone;

    private void Awake()
    {
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        XRGrabInteractable.selectEntered.AddListener(OnGrabbed);
        XRGrabInteractable.selectExited.AddListener(OnReleased);
    }

    private void OnGrabbed(SelectEnterEventArgs e)
    {
        if(SnapZone == null)
        {
            SnapZone = FindFirstObjectByType<SnapZone>();
        }
        if(SnapZone !=  null)
        {
            SnapZone.Highlight(true);
        }
    }

    private void OnReleased(SelectExitEventArgs e)
    {
        if(SnapZone != null)
        {
            SnapZone.Highlight(false);
            SnapZone.Snap();
        }
    }
}
