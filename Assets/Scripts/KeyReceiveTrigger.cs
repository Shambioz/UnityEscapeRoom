using UnityEngine;

public class KeyReceiveTrigger : MonoBehaviour
{
    public ScriptesAIPosition ScriptesAIPosition;

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
        if (other.CompareTag("Key"))
        {
            Debug.Log("KeyHere");
            // Perform your action here
            ScriptesAIPosition.receivedKey = true;
            ScriptesAIPosition.Startcutscene();
            Destroy(other.gameObject);
        }
    }
}
