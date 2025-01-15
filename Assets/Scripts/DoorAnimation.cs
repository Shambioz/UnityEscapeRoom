using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class DoorAnimation : MonoBehaviour
{
    private HingeJoint joint;
    private Rigidbody rb;
    public AudioSource AudioSource;
    public AudioClip AudioClip;
    public bool opened = false;
    private void Awake()
    {
        joint = GetComponent<HingeJoint>();
        rb = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("entered");
            if(AudioClip != null && AudioSource != null && !opened)
            { 
                AudioManager.Instance.PlaySound(AudioSource, AudioClip);
                joint.useMotor = true;
                opened = true;
            }
        }
    }
    private void Update()
    {
        if(joint.useMotor && gameObject.transform.rotation.z > 98)
        {
            rb.isKinematic = true;
            joint.useMotor = false;
        }
    }
}
