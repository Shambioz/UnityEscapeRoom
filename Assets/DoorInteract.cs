using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorInteract : MonoBehaviour
{
    public Transform doorTransform;
    public float angleOpened = 100f;
    public float movingTime = 0.5f;
    public AnimationCurve curve = AnimationCurve.EaseInOut(0,0,1,1);
    private XRGrabInteractable interactable;
    private bool isOpen = false;
    private Coroutine rotate;
    private Quaternion closed;
    private Quaternion opened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(doorTransform == null)
        {
            doorTransform = transform;
        }

        closed = doorTransform.localRotation;
        opened = closed * Quaternion.Euler(closed.x, angleOpened, closed.z);
        interactable = GetComponent<XRGrabInteractable>();
        if(interactable != null )
        {
            interactable.selectEntered.AddListener(OnInteracted);
        }
    }

    private void OnDestroy()
    {
        if(interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnInteracted);
        }
    }

    private void OnInteracted(SelectEnterEventArgs e)
    {
        isOpen = !isOpen;
        Quaternion target = isOpen ? opened : closed;
        if(rotate != null)
        {
            StopCoroutine(rotate);
        }
        rotate = StartCoroutine(RotateDoor(target, movingTime));
    }

    private IEnumerator RotateDoor(Quaternion target, float duration)
    {
        Quaternion start = doorTransform.localRotation;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float curveValue = curve.Evaluate(t);
            doorTransform.localRotation = Quaternion.Slerp(start, target, curveValue);
            yield return null;
        }
        doorTransform.localRotation = target;
        rotate = null;
    }
}
