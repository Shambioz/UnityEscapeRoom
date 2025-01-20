using UnityEngine;

public class ScriptesAIPosition : MonoBehaviour
{
    public RandomMoveAi aiScript;
    public Transform newTarget;
    public float delay = 5f;
    public bool playerdetected = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aiScript.SetScriptedTarget(newTarget.position, delay);
            playerdetected = true;
        }
    }
}
