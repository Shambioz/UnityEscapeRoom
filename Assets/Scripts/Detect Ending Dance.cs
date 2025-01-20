using UnityEngine;

public class DetectEndingDance : MonoBehaviour
{
    public Animator Animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        // Check if the entering object has the specific tag
        if (other.CompareTag("Oldman"))
        {
            Animator.enabled = true;
        }
    }


}
