using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance;
    public GameObject[] questItems;
    public GameEvents startEvent;
    public TextMeshProUGUI objectiveLabel;
    public GameEvents currentEvent;
    public SoundEmitter phoneDialogue;
    public int eventIndex = 0;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (currentEvent != null)
            StartEvent(currentEvent);
    }

    public void StartEvent(GameEvents gameEvent)
    {
        eventIndex++;
        currentEvent = gameEvent; // Set the current event to the one provided.
        currentEvent.StartObjective();
        UpdateObjectiveUI(gameEvent.objectivesText[gameEvent.currentObjectiveIndex  ],gameEvent.description);
        SoundManager.Instance.PlayNewObjectiveSound();
        switch (eventIndex)
        {
            case 4:
                for (int i = 0; i < 5; i++)
                {
                    questItems[i].GetComponent<InteractableObjects>().objectInteractable = true;
                }
                break;
        }
    }

    public void UpdateObjectiveUI(string newObjective, string description)
    {
        objectiveLabel.text = newObjective;
        SoundManager.Instance.PlayNewObjectiveSound(); 
    }

    // Call this when an objective is completed
    public void CompleteObjective()
    {
        Debug.Log("CompleteObjective called");
        if (startEvent != null)
        {
            currentEvent.AdvanceObjective();
            SoundManager.Instance.PlayObjectiveCompleteSound();
            Debug.Log("Advancing objective for: " + startEvent.description); 
        }
        
    }

  
}

