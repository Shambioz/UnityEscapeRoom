using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapZone : MonoBehaviour
{
    public Transform snapPoint;
    public MeshRenderer MeshRenderer;
    public bool isSnapped = false;
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
            Highlight(false);
            Snap();
        }
        else if (other.CompareTag("HipBag"))
        {
            Highlight(false);
            SnapHip();
        }
        else if (other.CompareTag("Glasses"))
        {
            Highlight(false);
            SnapGlasses();
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
            if (TaskManager.Instance != null)
            {
                TaskManager.Instance.CompleteTask("Fall Tracker");
            }
        }
    }

    public void SnapHip()
    {
        if (snapPoint != null)
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

    public void SnapGlasses()
    {
        if (snapPoint != null)
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
