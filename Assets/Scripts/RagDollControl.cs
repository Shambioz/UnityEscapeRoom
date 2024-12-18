using UnityEngine;
using UnityEngine.AI;
using UnityEngine.XR;

public class RagDollControl : MonoBehaviour
{
    private Rigidbody[] kinematics;
    public float deathtimer = 20;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    public Clock Clock;
    private RandomMoveAi RandomMoveAi;
    public GameObject Wolk;
    private AgentState currentstate = AgentState.Walking;
    public SkinnedMeshRenderer WolkMeshRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    void Awake()
    {
        kinematics = GetComponentsInChildren<Rigidbody>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        Clock = FindAnyObjectByType<Clock>();
        RandomMoveAi = GetComponent<RandomMoveAi>();
        Wolk = GameObject.Find("WolkHipProtection");
        WolkMeshRenderer = Wolk.GetComponent<SkinnedMeshRenderer>();
        DisableRagdoll();

    }

    void Start()
    {
        if (WolkMeshRenderer == null)
        {
            WolkMeshRenderer.SetBlendShapeWeight(0, 0f);
        }
    }

    private enum AgentState
    {
        Walking,
        Ragdoll
    }

    // Update is called once per frame
    void Update()
    {
        //Temp Input Method
        if (Clock.GameTime >= deathtimer)
        {
            Debug.Log("10 seconds have passed since the game started!");
            currentstate = AgentState.Ragdoll;
        }

        switch (currentstate) 
        {
            case AgentState.Walking:
                WalkingBehavior();
                break;

            case AgentState.Ragdoll:
                RagDollBehavior(); 
                break;
        }

        
    }

    private void DisableRagdoll()
    {
        foreach (var rigidbody in kinematics)
        {
            rigidbody.isKinematic = true;
        }
        navMeshAgent.enabled = true;
        animator.enabled = true;
        RandomMoveAi.enabled = true;
    }

    private void EnableRagdoll()
    {
        foreach (var rigidbody in kinematics)
        {
            rigidbody.isKinematic = false;
        }
        navMeshAgent.enabled = false;
        animator.enabled = false;
        RandomMoveAi.enabled = false;

        if (WolkMeshRenderer != null)
        {
            WolkMeshRenderer.SetBlendShapeWeight(0, 100f);
        }
    }

    private void WalkingBehavior()
    {
        DisableRagdoll();
        currentstate = AgentState.Walking;
    }

    private void RagDollBehavior()
    {
        EnableRagdoll();
        currentstate = AgentState.Ragdoll;
    }
}
