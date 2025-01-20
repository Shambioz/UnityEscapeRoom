using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class ScriptesAIPosition : MonoBehaviour
{
    public RandomMoveAi aiScript;
    public Animator animator;
    public RuntimeAnimatorController GoofyController;
    public Transform newTarget;
    public Transform newTarget2;
    public Transform newTarget3;
    public Transform newTarget4;
    public float delay = 5f;
    public bool playerdetected = false;
    public bool Arrived = false;
    public float que = 0f;
    private RecoverFall RecoverFall;
    public NavMeshObstacle obstacle;


    private void Awake()
    {
        RecoverFall = FindAnyObjectByType<RecoverFall>();
    }

    public void Startcutscene()
    {

        aiScript.SetScriptedTarget(newTarget.position, delay);
        aiScript.ScriptesAIPosition = this;
        obstacle.enabled = !obstacle.enabled;

    }
    /*
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aiScript.SetScriptedTarget(newTarget.position, delay);
            aiScript.ScriptesAIPosition = this;
        }
    }
    */
    private void Update()
    {
        if (que == 1 && newTarget2 != null) 
        {
            aiScript.SetScriptedTarget(newTarget2.position, delay);
            aiScript.ScriptesAIPosition = this;
            animator.runtimeAnimatorController = GoofyController;
        }

        if (que == 2 && newTarget3 != null)
        {
            aiScript.SetScriptedTarget(newTarget3.position, delay);
            aiScript.ScriptesAIPosition = this;
            animator.runtimeAnimatorController = GoofyController;
            RecoverFall.kill = true;
        }

        if (que == 3 && newTarget4 != null)
        {
            aiScript.SetScriptedTarget(newTarget4.position, delay);
            aiScript.ScriptesAIPosition = this;
        }
    }
}
