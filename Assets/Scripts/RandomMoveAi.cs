using UnityEngine;
using UnityEngine.AI;

public class RandomMoveAi : MonoBehaviour
{
    public float moveSpeed = 0.1f; // Speed at which the AI moves
    public float moveTime = 15f;  // Time to wait before changing direction
    public float roomSizeX = 10f; // Size of the room along the X axis
    public float roomSizeZ = 10f; // Size of the room along the Z axis
    public float rotationSpeed = 5f; // Speed at which the AI rotates towards its target
    public ScriptesAIPosition ScriptesAIPosition;

    public NavMeshAgent agent;   // Reference to the NavMeshAgent
    public float stuckTimeout = 20f; // Timeout before resetting target if stuck
    private float timeSinceLastMove; // Time since the last target change
    private float timeSinceTargetStart; // Time since the current target was set
    public float timeSinceTargetReached; // Time since the current target was set
    private bool scriptedTarget = false; // Flag to track if the AI is following a scripted target
    private float scriptedTargetDelay = 0f; // Time to wait at the scripted target before resuming random movement
    public bool randoMove = true;

    public Vector3 targetPosition;

    public float areaRadius = 10f;    // Radius within which the AI can move

    private Animator animator; // Reference to the Animator component

    void Start()
    {
        // Get the Animator component
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        // Set an initial target position for the AI to move towards
        SetRandomTargetPosition();
    }

    void Update()
    {
        // Increase the time since the last move was made
        timeSinceLastMove += Time.deltaTime;
        timeSinceTargetStart += Time.deltaTime;
        timeSinceTargetReached += Time.deltaTime;

        // Move the AI towards the target
        MoveTowardsTarget();

        // Update the Animator Speed parameters
        UpdateAnimator();

        // Check if the AI has reached the target
        if (HasReachedTarget())
        {
            if (scriptedTarget)
            {
                ScriptesAIPosition.Arrived = true;
                agent.speed = 0;
                // If the AI is at a scripted target, wait for the delay before resuming random movement
                if (timeSinceTargetReached >= scriptedTargetDelay)
                {
                    ScriptesAIPosition.que++;
                    ScriptesAIPosition.Arrived = false;
                    timeSinceTargetReached = 0f;
                    timeSinceLastMove = 0f; // Reset the timer
                    timeSinceTargetStart = 0f; // Reset the stuck timer
                    scriptedTarget = false; // Resume random movement
                    SetRandomTargetPosition();
                    agent.speed = moveSpeed;
                }
            }
            else if (randoMove == true)
            {
                agent.speed = 0;
                // If the AI is at a random target, set a new random target after the moveTime
                if (timeSinceLastMove >= moveTime)
                {
                    SetRandomTargetPosition();
                    timeSinceLastMove = 0f; // Reset the timer
                    timeSinceTargetStart = 0f; // Reset the stuck timer
                    agent.speed = moveSpeed;
                }
            }
        }

        // If the AI has been trying to reach the target for too long and hasn't arrived, change the target
        if (timeSinceTargetStart >= stuckTimeout && !HasReachedTarget() && !scriptedTarget && randoMove == true)
        {
            SetRandomTargetPosition();
            timeSinceLastMove = 0f; // Reset the timer
            timeSinceTargetStart = 0f; // Reset the stuck timer
        }
    }

    // Moves the AI towards the target position and rotates it
    void MoveTowardsTarget()
    {
        // Move the AI towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Rotate the AI to face the target position smoothly
        Vector3 directionToTarget = targetPosition - transform.position;
        if (directionToTarget != Vector3.zero && agent.speed != 0) // Avoid errors when at the target position
        {
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget); // Calculate the rotation needed
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime); // Smoothly rotate the AI
        }
    }

    // Sets a new random target position within the room's boundaries
    void SetRandomTargetPosition()
    {
        agent.speed = moveSpeed;
        targetPosition = GetRandomNavMeshPosition();
        agent.SetDestination(targetPosition); // Move the agent to the new target
    }

    Vector3 GetRandomNavMeshPosition()
    {
        // Random position within a radius around the AI's current position
        Vector3 randomDirection = Random.insideUnitSphere * areaRadius;
        randomDirection += transform.position; // Add to the current position

        // Find the closest point on the NavMesh to the random position
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, areaRadius, NavMesh.AllAreas))
        {
            return hit.position; // Return the valid position found on the NavMesh
        }

        // If no valid position is found, return the current position (fallback)
        return transform.position;
    }

    // Check if the AI has reached the target
    bool HasReachedTarget()
    {
        // Check if the agent is close enough to the target (you can adjust this threshold)
        if (Vector3.Distance(agent.transform.position, targetPosition) < 1f)
        {
            return true;
        }
        return false;
    }

    // Update the Animator Speed parameters based on movement direction
    void UpdateAnimator()
    {
        // Calculate the movement vector
        Vector3 movementDirection = targetPosition - transform.position;

        // Set the SpeedX and SpeedY parameters in the Animator
        // SpeedX (horizontal movement)
        animator.SetFloat("SpeedX", movementDirection.x);

        // SpeedY (vertical movement)
        animator.SetFloat("SpeedY", movementDirection.z);
    }

    // Public method to set a scripted target position and delay
    public void SetScriptedTarget(Vector3 newTarget, float delay)
    {
        agent.speed = moveSpeed;
        scriptedTarget = true; // Set the flag to indicate a scripted target
        targetPosition = newTarget; // Set the new target position
        agent.SetDestination(targetPosition); // Move the agent to the new target
        scriptedTargetDelay = delay; // Set the delay time
        timeSinceLastMove = 0f; // Reset the timer
        timeSinceTargetStart = 0f; // Reset the stuck timer
        timeSinceTargetReached = 0f; // Reset the target reached time
    }
}
