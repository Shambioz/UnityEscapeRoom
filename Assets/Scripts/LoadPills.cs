using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LoadPills : MonoBehaviour
{
    public PillDispenser dispenser;
    public Transform snapPos1;
    public Transform snapPos2;
    private bool isMoving = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pills") && dispenser.currentState == PillDispenserState.Opened)
        {
            dispenser.currentState = PillDispenserState.Transition;
            Transform parent = other.gameObject.transform.parent;
            SnapObject(parent.gameObject);
        }
    }

    private void SnapObject(GameObject obj)
    {
        obj.transform.position = snapPos1.position;
        obj.transform.rotation = snapPos1.rotation;
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        XRGrabInteractable xRGrabInteractable = obj.GetComponent<XRGrabInteractable>();
        xRGrabInteractable.enabled = false;
        Collider[] colliders = obj.GetComponentsInChildren<Collider>();
        foreach(Collider collider in colliders)
        {
            collider.enabled = false;
        }
        StartCoroutine(MoveToTarget(obj));
    }

    private IEnumerator MoveToTarget(GameObject obj)
    {
        isMoving = true;
        Vector3 startPosition = snapPos1.position;
        Vector3 endPosition = snapPos2.position;
        obj.transform.position = startPosition;
        obj.transform.rotation = snapPos1.rotation;
        float distance = Vector3.Distance(startPosition, endPosition);
        float travelTime = 3f;
        float elapsedTime = 0f;
        while (elapsedTime < travelTime)
        {
            float t = elapsedTime / travelTime;
            obj.transform.position = Vector3.Lerp(startPosition, endPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        obj.transform.position = endPosition;
        isMoving = false;
        dispenser.currentState = PillDispenserState.Loaded;
        dispenser.CloseLid();
        dispenser.LoadPills();
    }
}
