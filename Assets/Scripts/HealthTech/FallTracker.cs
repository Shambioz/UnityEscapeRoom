using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class FallTracker : MonoBehaviour
{
    private XRGrabInteractable XRGrabInteractable;
    [SerializeField] private SnapZone SnapZone;

    private void Awake()
    {
        XRGrabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void OnEnable()
    {
        XRGrabInteractable.selectEntered.AddListener(OnGrabbed);
        XRGrabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrabbed(SelectEnterEventArgs e)
    {
        if(SnapZone !=  null)
        {
            MeshRenderer meshRenderer = SnapZone.MeshRenderer;
            SnapZone.Highlight(true, meshRenderer);
        }
    }

    private void OnRelease(SelectExitEventArgs e)
    {
        if (SnapZone != null)
        {
            MeshRenderer meshRenderer = SnapZone.MeshRenderer;
            SnapZone.Highlight(false, meshRenderer);
        }
    }
}
