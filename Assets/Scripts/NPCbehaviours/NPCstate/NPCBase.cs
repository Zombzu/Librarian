using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCBase : MonoBehaviour
{
    public List<Transform> waypoints;
    public NPCState idleState;
    public NPCState roamingState;
    public NPCState talkingState;
    public bool isPlayerNear = false;

    private NavMeshAgent agent;
    private Animator animator;
    private NPCState currentState;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        idleState = gameObject.AddComponent<NPCIdle>();
        roamingState = gameObject.AddComponent<NPCRoamingState>();
        talkingState = gameObject.AddComponent<NPCTalkingState>();

        idleState.Initialize(this, agent, animator);
        roamingState.Initialize(this, agent, animator);
        talkingState.Initialize(this, agent, animator);

        TransitionToState(roamingState);
    }

    void Update()
    {
        currentState.UpdateState();
    }

    public void TransitionToState(NPCState newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }
        currentState = newState;
        currentState.EnterState();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            TransitionToState(talkingState);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
        }
    }
}
