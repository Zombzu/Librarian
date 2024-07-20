using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTalkingState : NPCState
{
    public DialogueManager dialogueManager; // Reference to the DialogueManager
    public string[] npcResponses; // Dialogue responses
    private int currentResponseIndex = 0;

    public override void EnterState()
    {
        agent.isStopped = true;
        animator.SetBool("IsTalking", true);
        animator.SetBool("IsRoaming", false);
        animator.SetBool("IsIdle", false);

        if (dialogueManager != null && npcResponses.Length > 0)
        {
            dialogueManager.DisplayDialogue(npcResponses[currentResponseIndex]); // Display first response
        }
    }

    public override void UpdateState()
    {
        if (!npcBase.isPlayerNear)
        {
            npcBase.TransitionToState(npcBase.idleState);
            return;
        }
        
        // Check for player input to continue dialogue
        if (Input.GetKeyDown(KeyCode.E) && npcBase.isPlayerNear) // Example: Press 'E' to continue dialogue
        {
            if (dialogueManager != null)
            {
                currentResponseIndex++;
                if (currentResponseIndex < npcResponses.Length)
                {
                    dialogueManager.DisplayDialogue(npcResponses[currentResponseIndex]); // Display next response
                }
                else
                {
                    dialogueManager.DisplayDialogue("No more responses.");
                    // Optionally transition back to idle or roaming state
                    npcBase.TransitionToState(npcBase.idleState);
                }
            }
        }
    }

    public override void ExitState()
    {
        agent.isStopped = false;
        animator.SetBool("IsTalking", false);
        if (dialogueManager != null)
        {
            dialogueManager.ClearDialogue(); // Clear dialogue when exiting the state
            dialogueManager.HideDialogue(); // Hide dialogue UI
        }
    }
}
