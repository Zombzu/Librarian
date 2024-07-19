using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdle : NPCState
{
    private float idleTimer;
    private float idleDuration = 5f; // Idle for 5 seconds

    public override void EnterState()
    {
        animator.SetBool("IsIdle", true);
        animator.SetBool("IsRoaming", false);
        animator.SetBool("IsTalking", false);
        idleTimer = 0f;
    }

    public override void UpdateState()
    {
        idleTimer += Time.deltaTime;
        if (idleTimer >= idleDuration)
        {
            npcBase.TransitionToState(npcBase.roamingState);
        }
    }

    public override void ExitState()
    {
        animator.SetBool("IsIdle", false);
    }
}
