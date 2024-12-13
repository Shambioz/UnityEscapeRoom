using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class LidClamper : MonoBehaviour
{
    public Transform objectToMove;
    public Transform snappingPosition;
    public Transform originalPosition;
    private XRGrabInteractable interactable;
    private bool isSnapped = false;
    private Coroutine moving;
    public float moveTime = 0.5f;
    public AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    void Start()
    {
        if (objectToMove == null)
        {
            objectToMove = transform;
        }
        interactable = GetComponent<XRGrabInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnInteracted);
        }
    }

    void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(OnInteracted);
        }
    }
    private void OnInteracted(SelectEnterEventArgs e)
    {
        isSnapped = !isSnapped;
        Vector3 targetPos = isSnapped ? snappingPosition.position : originalPosition.position;
        if(moving != null)
        {
            StopCoroutine(moving);
        }
        moving = StartCoroutine(Move(targetPos, moveTime));
    }

    private IEnumerator Move(Vector3 target, float time)
    {
        Vector3 startPosition = objectToMove.position;
        float elapsed = 0f;
        while(elapsed < time)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / time;
            float curve = movementCurve.Evaluate(t);
            objectToMove.position = Vector3.Lerp(startPosition, target, curve);
            yield return null;
        }
        objectToMove.position = target;
        moving = null;
    }
}