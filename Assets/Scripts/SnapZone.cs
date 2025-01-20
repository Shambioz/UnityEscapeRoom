using UnityEditor.XR.Interaction.Toolkit;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class SnapZone : MonoBehaviour
{
    [SerializeField] private Transform snapPoint;
    [SerializeField] private Transform snapPointHip;
    [SerializeField] private Transform snapPointGlasses;
    [SerializeField] private MeshRenderer fallTrackerArea;
    [SerializeField] private MeshRenderer hip;
    [SerializeField] private MeshRenderer glasses;
    public MeshRenderer MeshRenderer;
    public bool isSnapped = false;
    public GameObject fallTracker;

    public void Highlight(bool highlight, MeshRenderer meshRenderer)
    {
         meshRenderer.enabled = highlight;
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FallTracker"))
        {
            Highlight(false, fallTrackerArea);
            Snap(other.gameObject);
        }
        else if (other.CompareTag("HipBag"))
        {
            Highlight(false, hip);
            SnapHip(other.gameObject);
        }
        else if (other.CompareTag("Glasses"))
        {
            Highlight(false, glasses);
            SnapGlasses(other.gameObject);
        }
    }

    public void Snap(GameObject snappedObject)
    {
        if(snapPoint != null)
        {
            snappedObject.transform.position = snapPoint.position;
            snappedObject.transform.rotation = snapPoint.rotation;
            snappedObject.transform.SetParent(gameObject.transform);
            Rigidbody rb = snappedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            XRGrabInteractable XRGrabInteractable = snappedObject.GetComponent<XRGrabInteractable>();
            XRGrabInteractable.enabled = false;
            if (TaskManager.Instance != null)
            {
                TaskManager.Instance.CompleteTask("Fall Tracker");
            }
        }
    }

    public void SnapHip(GameObject snappedObject)
    {
        if (snapPointHip != null)
        {
            snappedObject.transform.position = snapPointHip.position;
            snappedObject.transform.rotation = snapPointHip.rotation;
            snappedObject.transform.SetParent(gameObject.transform);
            Rigidbody rb = snappedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            XRGrabInteractable XRGrabInteractable = snappedObject.GetComponent<XRGrabInteractable>();
            XRGrabInteractable.enabled = false;
        }
    }

    public void SnapGlasses(GameObject snappedObject)
    {
        if (snapPointGlasses != null)
        {
            snappedObject.transform.position = snapPointGlasses.position;
            snappedObject.transform.rotation = snapPointGlasses.rotation;
            snappedObject.transform.SetParent(gameObject.transform);
            Rigidbody rb = snappedObject.GetComponent<Rigidbody>();
            rb.isKinematic = true;
            rb.useGravity = false;
            XRGrabInteractable XRGrabInteractable = snappedObject.GetComponent<XRGrabInteractable>();
            XRGrabInteractable.enabled = false;
            if (TaskManager.Instance != null)
            {
                TaskManager.Instance.CompleteTask("Glasses");
            }
        }
    }
}
