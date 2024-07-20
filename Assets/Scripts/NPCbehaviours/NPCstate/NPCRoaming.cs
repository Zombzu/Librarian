using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCRoamingState : NPCState
{
    private int currentWaypointIndex = 0;
    private float waypointTolerance = 1f;

    public override void EnterState()
    {
        animator.SetBool("IsRoaming", true);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsTalking", false);
        MoveToNextWaypoint();
    }

    public override void UpdateState()
    {
        if (agent.remainingDistance <= waypointTolerance)
        {
            npcBase.TransitionToState(npcBase.idleState);
        }
    }

    public override void ExitState()
    {
        // Any exit logic if needed
    }

    private void MoveToNextWaypoint()
    {
        if (npcBase.waypoints.Count == 0) return;

        currentWaypointIndex = (currentWaypointIndex + 1) % npcBase.waypoints.Count;
        agent.SetDestination(npcBase.waypoints[currentWaypointIndex].position);
    }
}
