using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorInteract : MonoBehaviour
{
    private const string DOOR = "Door";
    public Transform doorTransform;
    public float angleOpened = 100f;
    public float movingTime = 0.5f;
    public Animator animator;
    
    private XRGrabInteractable interactable;
    private bool isOpen = false;
    private Coroutine rotate;
    private Quaternion closed;
    private Quaternion opened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(animator == null)
        {
            animator = GetComponent<Animator>();
        }
        animator.SetBool(DOOR, false);
        if(doorTransform == null)
        {
            doorTransform = transform;
        }

        /*closed = doorTransform.localRotation;
        opened = closed * Quaternion.Euler(0, 0, angleOpened);*/
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
        Debug.Log("sure");
        isOpen = !isOpen;
        if(isOpen)
        {
            animator.SetBool(DOOR, true);
        }
        if (!isOpen)
        {
            animator.SetBool(DOOR, false);
        }

        /* Quaternion target = isOpen ? opened : closed;
         if(rotate != null)
         {
             StopCoroutine(rotate);
         }
         rotate = StartCoroutine(RotateDoor(target, movingTime));*/
    }
    /*private IEnumerator RotateDoor(Quaternion target, float duration)
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
    }*/
}
