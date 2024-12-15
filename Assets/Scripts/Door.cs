using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Transform doorTrnsform;
    public float angleOpen = 100f;
    public float duration = 1.0f;
    public AnimationCurve openAnimation = AnimationCurve.EaseInOut(0, 0, 1, 1);
    private bool isOpen = false;
    private bool isAnimating = false;
    public int count = 0;
    private Quaternion angleClosed;
    private Quaternion angleOpened;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        angleClosed = doorTrnsform.localRotation;
        angleOpened = Quaternion.Euler(0, angleOpen, 0) * angleClosed;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("ENTER: " + other.name);
        if (other.CompareTag("Player") || other.CompareTag("OldMan"))
        {
            int oldCount = count;
            count++;
            if(oldCount == 0 && count == 1 && !isOpen)
            {
                StartCoroutine(Open());
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("EXIT: " + other.name);
        if (other.CompareTag("Player") || other.CompareTag("OldMan"))
        {
            int oldCount = count;
            count = Mathf.Max(count - 1, 0);
            if(oldCount == 1 && count == 0 && isOpen)
            {
                StartCoroutine(Close());
            }
        }
    }

    private IEnumerator Open()
    {
        isOpen = true;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float curveValue = openAnimation.Evaluate(t);
            doorTrnsform.localRotation = Quaternion.Slerp(angleClosed, angleOpened, curveValue);
            yield return null;
        }
        doorTrnsform.localRotation = angleOpened;
    }

    private IEnumerator Close()
    {
        isOpen = false;
        float elapsed = 0f;
        while(elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;
            float curveValue = openAnimation.Evaluate(t);
            doorTrnsform.localRotation = Quaternion.Slerp(angleOpened, angleClosed, curveValue);
            yield return null;
        }
        doorTrnsform.localRotation = angleClosed;
    }
}
