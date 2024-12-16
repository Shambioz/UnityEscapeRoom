using UnityEngine;

public class SpawnDataPoints : MonoBehaviour
{
    // The prefab to spawn
    public GameObject objectToSpawn;

    // The time interval between spawns
    public float spawnInterval = 5f;

    // The vertical offset for the spawned object
    public float spawnHeightOffset = 1f;

    // Gradient to determine the color
    public Gradient colorGradient;

    // Maximum time duration (used to normalize time)
    public float maxTime = 60f;

    //Rate it increase hight
    public float HightRate = 60f;

    // Tracks elapsed time since the spawner started
    private float elapsedTime = 0f;

    // Start is called before the first frame update
    void Start()
    {
        // Start the spawning process
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    // Method to spawn the object
    void SpawnObject()
    {
        if (objectToSpawn != null)
        {
            // Calculate the spawn position above this object
            Vector3 spawnPosition = new Vector3(transform.position.x, spawnHeightOffset, transform.position.z);

            // Instantiate the object at the calculated position
            GameObject spawnedObject = Instantiate(objectToSpawn, spawnPosition, Quaternion.identity);

            // Update elapsed time and normalize it based on maxTime
            elapsedTime += spawnInterval;
            float normalizedTime = Mathf.Clamp01(elapsedTime / maxTime);

            //Increase hight overtime
            spawnHeightOffset = (Time.fixedDeltaTime / HightRate) + spawnHeightOffset;

            // Get the color from the gradient
            Color newColor = colorGradient.Evaluate(normalizedTime);

            // Apply the new color to the object's material
            Renderer objectRenderer = spawnedObject.GetComponent<Renderer>();
            if (objectRenderer != null)
            {
                // Assign a new material instance to avoid changing the prefab's material
                objectRenderer.material = new Material(objectRenderer.material);
                objectRenderer.material.color = newColor;
            }
        }
        else
        {
            Debug.LogWarning("Object to spawn is not assigned!");
        }
    }
}
