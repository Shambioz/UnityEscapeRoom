using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public enum PillDispenserState
{
    Empty,
    Loaded,
    Dispensing
}
public class PillDispenser : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    public GameObject pill;
    public Transform dispense;
    public PillDispenserState currentState = PillDispenserState.Empty;
    public int currentPills = 0;
    public int maxPills = 10;
    public AudioClip NoPills;
    public AudioClip PillsLoaded;
    public AudioClip PillsDispensing;
    public AudioSource audioSource;
    public AudioManager audioManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(grabInteractable != null)
        {
            grabInteractable.selectEntered.AddListener(OnGrabbed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pills"))
        {
            Destroy(other.gameObject);
            LoadPills();
        }
    }


    private void OnGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Grabbed");
        if(currentState == PillDispenserState.Empty)
        {
            audioManager.PlaySound(audioSource, NoPills);
        }
        else if(currentState == PillDispenserState.Loaded)
        {
            StartDispensing();
        }
    }

    public void LoadPills()
    {
        currentPills = 10;
        if(currentPills > 0)
        {
            currentState = PillDispenserState.Loaded;
            audioManager.PlaySound(audioSource, PillsLoaded);
        }
    }

    public void StartDispensing()
    {
        if(currentState == PillDispenserState.Loaded && currentPills > 0)
        {
            currentState = PillDispenserState.Dispensing;
            StartCoroutine(Dispense());
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
                currentState = PillDispenserState.Empty;
            }
            else
            {
                currentState = PillDispenserState.Loaded;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
