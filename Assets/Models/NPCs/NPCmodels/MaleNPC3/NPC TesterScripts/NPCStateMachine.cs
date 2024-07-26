using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class NPCStateMachine : MonoBehaviour
{
    public NPCStateTest currentState;

    // Animation and other references
    public Animator animator;
    public GameObject bookPrefab;
    public Transform bookSpawnPoint;
    public Transform deskTransform;
    public Transform exitPoint;
    public float movementSpeed = 3.5f;

    private NavMeshAgent navMeshAgent;
    private bool isMoving = false; // Track if the NPC is moving

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        SetState(NPCStateTest.Idle);
    }

    void Update()
    {
        switch (currentState)
        {
            case NPCStateTest.Idle:
                // Idle logic: NPC might be waiting for input or events
                if (ShouldStartWalking()) // Check for a condition to start walking
                {
                    SetState(NPCStateTest.WalkIn);
                }
                break;

            case NPCStateTest.Walking:
                // Walking logic: Move to a specific target
                if (!isMoving)
                {
                    MoveTo(deskTransform.position);
                    isMoving = true;
                }
                break;

            case NPCStateTest.Talking:
                // Talking logic: NPC might be playing a talking animation or showing dialogue
                if (ShouldFinishTalking()) // Check for a condition to finish talking
                {
                    SetState(NPCStateTest.Idle);
                }
                break;

            case NPCStateTest.WalkIn:
                // Handle walking into the desk
                if (!isMoving)
                {
                    MoveTo(deskTransform.position, () =>
                    {
                        SetState(NPCStateTest.CheckingOut);
                    });
                }
                break;

            case NPCStateTest.CheckingOut:
                // Handle the checking out process
                if (!isMoving)
                {
                    // Simulate some time for checking out
                    StartCoroutine(CheckingOutRoutine());
                }
                break;

            case NPCStateTest.WalkOut:
                // Handle walking out of the scene
                if (!isMoving)
                {
                    MoveTo(exitPoint.position, () =>
                    {
                        SetState(NPCStateTest.Idle);
                    });
                }
                break;
        }
    }

    void SetState(NPCStateTest newState)
    {
        // Exit the current state
        ExitCurrentState();

        // Transition to the new state
        currentState = newState;
        UpdateAnimator();

        // Enter the new state
        EnterNewState();
    }

    void UpdateAnimator()
    {
        // Update animator parameters based on the current state
        animator.SetBool("IsWalking", currentState == NPCStateTest.Walking);
        animator.SetBool("IsTalking", currentState == NPCStateTest.Talking);
        animator.SetBool("IsCheckingOut", currentState == NPCStateTest.CheckingOut);
        animator.SetBool("IsWalkingIn", currentState == NPCStateTest.WalkIn);
        animator.SetBool("IsWalkingOut", currentState == NPCStateTest.WalkOut);
        // Add more parameters as needed
    }

    void ExitCurrentState()
    {
        // Logic for exiting the current state, if needed
        if (currentState == NPCStateTest.Walking || currentState == NPCStateTest.WalkIn || currentState == NPCStateTest.WalkOut)
        {
            isMoving = false;
            navMeshAgent.isStopped = true; // Stop the NavMeshAgent
        }

        // Reset animations when exiting a state
        if (currentState == NPCStateTest.CheckingOut)
        {
            animator.SetBool("IsCheckingOut", false);
        }
    }

    void EnterNewState()
    {
        // Logic for entering the new state, if needed
        if (currentState == NPCStateTest.Walking)
        {
            animator.SetTrigger("StartWalking");
        }
        else if (currentState == NPCStateTest.Talking)
        {
            animator.SetTrigger("StartTalking");
        }
        else if (currentState == NPCStateTest.WalkIn)
        {
            animator.SetTrigger("StartWalkingIn");
        }
        else if (currentState == NPCStateTest.CheckingOut)
        {
            animator.SetTrigger("StartCheckingOut");
        }
        else if (currentState == NPCStateTest.WalkOut)
        {
            animator.SetTrigger("StartWalkingOut");
        }
    }

    void MoveTo(Vector3 targetPosition, System.Action onArrived = null)
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.destination = targetPosition;
            navMeshAgent.speed = movementSpeed;
            navMeshAgent.isStopped = false; // Ensure the agent is not stopped
            isMoving = true;

            StartCoroutine(WaitForArrival(onArrived));
        }
        else
        {
            // Handle manual movement if not using NavMeshAgent
        }
    }

    IEnumerator WaitForArrival(System.Action onArrived)
    {
        while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.1f)
        {
            yield return null;
        }
        isMoving = false;
        navMeshAgent.isStopped = true; // Stop the NavMeshAgent when destination is reached
        onArrived?.Invoke();
    }

    void SpawnBook()
    {
        Instantiate(bookPrefab, bookSpawnPoint.position, Quaternion.identity);
    }

    bool ShouldStartWalking()
    {
        // Custom logic to determine when the NPC should start walking
        // For example, you might use a timer or check for an event
        // Placeholder logic: start walking when idle for a certain time
        return Time.timeSinceLevelLoad > 5.0f; // Example condition
    }

    bool ShouldFinishTalking()
    {
        // Custom logic to determine when the NPC should finish talking
        // For example, you might use a timer or check for an event
        return Time.timeSinceLevelLoad > 10.0f; // Example condition
    }

    IEnumerator CheckingOutRoutine()
    {
        // Simulate the checking out process
        // Wait for a short duration to simulate the checkout process
        yield return new WaitForSeconds(2.0f);

        SpawnBook(); // Spawn the book on the table
        SetState(NPCStateTest.WalkOut); // Transition to the WalkOut state
    }
}
