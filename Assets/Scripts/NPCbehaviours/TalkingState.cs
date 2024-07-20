using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingState : StateMachineBehaviour
{
    Transform playerTransform;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Get the player's transform for proximity checks
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        // Stop the NPC's movement while talking
        animator.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = true;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Handle talking logic here, e.g., facing the player, dialog system
        if (Vector3.Distance(animator.transform.position, playerTransform.position) > 5f) // If player moves away
        {
            animator.SetBool("IsTalking", false);
            animator.SetBool("IsRoaming", true);
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Resume NPC's movement when talking ends
        animator.GetComponent<UnityEngine.AI.NavMeshAgent>().isStopped = false;
    }
}
