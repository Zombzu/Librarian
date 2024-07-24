using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NPCTalkingState : NPCState
{
    public DialogueManager dialogueManager; 
    public string[] npcResponses; 
    private int currentResponseIndex = 0;

    public override void EnterState()
    {
        agent.isStopped = true;
        animator.SetBool("IsTalking", true);
        animator.SetBool("IsRoaming", false);
        animator.SetBool("IsIdle", false);

        if (dialogueManager != null && npcResponses.Length > 0)
        {
            dialogueManager.DisplayDialogue(npcResponses[currentResponseIndex]); 
        }
    }

    public override void UpdateState()
    {
        if (!npcBase.isPlayerNear)
        {
            npcBase.TransitionToState(npcBase.idleState);
            return;
        }
        
        
        if (Input.GetKeyDown(KeyCode.E) && npcBase.isPlayerNear) 
        {
            if (dialogueManager != null)
            {
                currentResponseIndex++;
                if (currentResponseIndex < npcResponses.Length)
                {
                    dialogueManager.DisplayDialogue(npcResponses[currentResponseIndex]);
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
            dialogueManager.ClearDialogue(); 
            dialogueManager.HideDialogue(); 
        }
    }
}
