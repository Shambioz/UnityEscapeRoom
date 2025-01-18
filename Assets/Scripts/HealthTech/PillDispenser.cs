using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public enum PillDispenserState
{
    Closed,
    Opened,
    Transition,
    Loaded,
    Dispensing,
    Dispensed
}
public class PillDispenser : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public XRGrabInteractable lidInteractable;
    public GameObject pill;
    public GameObject Lid;
    public Transform dispense;
    public PillDispenserState currentState = PillDispenserState.Opened;
    public int currentPills = 0;
    public int maxPills = 3;
    public AudioClip NoPills;
    public AudioClip PillsLoaded;
    public AudioClip PillsDispensing;
    public AudioClip ItsTime;
    public AudioSource audioSource;
    public AudioManager audioManager;
    public GameObject colliderForPills;
    public Canvas canvas0;
    public Canvas canvas1;
    public Canvas canvas2;
    public Canvas canvas3;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        AudioManager.Instance.PlaySound(audioSource, ItsTime);
        lidInteractable = Lid.GetComponent<XRGrabInteractable>();
        canvas0.gameObject.SetActive(true);
        canvas1.gameObject.SetActive(false);
        canvas2.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(false);
        if(lidInteractable != null)
        {
            lidInteractable.selectEntered.AddListener(OnLidGrabbed);
        }
    }

    private void OnLidGrabbed(SelectEnterEventArgs args)
    {
        if(currentState == PillDispenserState.Opened || currentState == PillDispenserState.Closed)
        {
            HingeJoint joint = Lid.GetComponent<HingeJoint>();
            JointMotor jointMotor = joint.motor;
            jointMotor.targetVelocity = jointMotor.targetVelocity * -1;
            joint.motor = jointMotor;

            if (currentState == PillDispenserState.Closed)
            {
                currentState = PillDispenserState.Opened;
            }
            else if(currentState == PillDispenserState.Opened)
            {
                currentState = PillDispenserState.Closed;
            }
        }
    }

    public void CloseLid()
    {
        HingeJoint joint = Lid.GetComponent<HingeJoint>();
        JointMotor jointMotor = joint.motor;
        jointMotor.targetVelocity = jointMotor.targetVelocity * -1;
        joint.motor = jointMotor;
    }

    public void LoadPills()
    {
        canvas0.gameObject.SetActive(false);
        canvas1.gameObject.SetActive(true);
        currentPills = maxPills;
        if(currentPills > 0)
        {
            currentState = PillDispenserState.Loaded;
            audioManager.PlaySound(audioSource, PillsLoaded);
        }
    }

    public void StartDispensing()
    {
        Debug.Log("registered");
        if(currentState == PillDispenserState.Loaded && currentPills > 0)
        {
            currentState = PillDispenserState.Dispensing;
            StartCoroutine(Dispense());
        }
        else
        {
            AudioManager.Instance.PlaySound(audioSource, NoPills);
        }
    }

    private IEnumerator Dispense()
    {
        if(currentState == PillDispenserState.Dispensing && currentPills > 0)
        {
            audioManager.PlaySound(audioSource, PillsDispensing);
            while(currentPills > 0 && currentState == PillDispenserState.Dispensing)
            {
                Instantiate(pill, dispense.position, dispense.rotation);
                currentPills--;
                yield return new WaitForSeconds(0.2f);
            }
            if(currentPills <= 0)
            {
                currentState = PillDispenserState.Closed;
                canvas1.gameObject.SetActive(false);
                canvas2.gameObject.SetActive(true);
                if (TaskManager.Instance != null)
                {
                    TaskManager.Instance.CompleteTask("Pill Dispenser");
                }
            }
            else
            {
                currentState = PillDispenserState.Loaded;
            }
        }
    }

    public void ChangeUI()
    {
        canvas2.gameObject.SetActive(false);
        canvas3.gameObject.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
