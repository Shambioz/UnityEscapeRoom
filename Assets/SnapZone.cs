using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapZone : MonoBehaviour
{
    public Transform snapPoint;
    public MeshRenderer MeshRenderer;
    private bool isSnapped = false;
    public GameObject fallTracker;

    public void Highlight(bool highlight)
    {
        if(MeshRenderer != null)
        {
            MeshRenderer.enabled = highlight;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallTracker"))
        {
            isSnapped = true;
        }
    }

    public void Snap()
    {
        if(snapPoint != null)
        {
            fallTracker.transform.position = snapPoint.position;
            fallTracker.transform.rotation = snapPoint.rotation;
            fallTracker.transform.SetParent(gameObject.transform);
            Rigidbody rb = fallTracker.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            XRGrabInteractable XRGrabInteractable = fallTracker.GetComponent<XRGrabInteractable>();
            XRGrabInteractable.enabled = false;
        }
    }

    
}
