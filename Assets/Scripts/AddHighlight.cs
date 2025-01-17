using System.Collections;
using UnityEditor.SpeedTree.Importer;
using UnityEngine;

public class AddHighlight : MonoBehaviour
{
    public Material blinkingMaterial; // The material to blink with
    private Material originalMaterial; // The original material of the object
    public Renderer objectRenderer; // The renderer component of the object

    public float blinkInterval = 0.5f; // Time interval for blinking
    private float timer;
    private bool isBlinking = false;
    void Start()
    {
        // Get the Renderer component of the GameObject
        if(objectRenderer == null)
        {
            objectRenderer = GetComponent<Renderer>();
        }

        // Store the original material
        if (objectRenderer != null)
        {
            originalMaterial = objectRenderer.material;
        }

    }

    void Update()
    {
        if (isBlinking)
        {
            // Update the timer
            timer += Time.deltaTime;

            // Toggle the material when the timer exceeds the blink interval
            if (timer >= blinkInterval)
            {
                ToggleMaterial();
                timer = 0f; // Reset the timer
            }
        }
    }

    // Toggle between the original material and the blinking material
    private void ToggleMaterial()
    {
        if (objectRenderer.material == originalMaterial)
        {
            objectRenderer.material = blinkingMaterial;
        }
        else
        {
            objectRenderer.material = originalMaterial;
        }
    }

    // Public method to start/stop the blinking effect
    public void ToggleBlinking()
    {
        /*isBlinking = !isBlinking;
        // Reset to the original material when stopping the blinking
        if (!isBlinking && objectRenderer != null)
        {
            objectRenderer.material = originalMaterial;
        }
        float timerr = 0f;
        while(timerr <= 300f)
        {
            Debug.Log("timer");
            timerr += Time.deltaTime;
        }
        isBlinking = !isBlinking;
        objectRenderer.material = originalMaterial;*/
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        isBlinking = !isBlinking;
        yield return new WaitForSeconds(5f);
        isBlinking = !isBlinking;
        objectRenderer.material = originalMaterial;

    }
}
