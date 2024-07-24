using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 
using TMPro; 

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; 
    public GameObject dialoguePanel;

    private void Start()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Hide panel initially
        }
    }

    public void DisplayDialogue(string dialogue)
    {
        if (dialogueText != null)
        {
            dialogueText.text = dialogue;
        }
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true); // Show panel
        }
    }

    public void HideDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // Hide panel
        }
    }

    public void ClearDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = "";
        }
    }
}
