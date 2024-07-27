using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[CreateAssetMenu(fileName = "GameEvent", menuName = "ScriptableObjects/GameEvent", order = 1)]
public class GameEvents : ScriptableObject
{
    public string description;
    public GameEvents nextEvent;
    public bool isObjectiveComplete = false;
    public string[] objectivesText; // Different steps in the event
    public int currentObjectiveIndex = 0;
    public string soundEmitterName = null;
    public bool playSound = false;
    private SoundEmitter soundEmitter;
    public void AdvanceObjective()
    {    Debug.Log("Current Objective Index: " + currentObjectiveIndex);
        if (currentObjectiveIndex < objectivesText.Length - 1)
        {
            currentObjectiveIndex++;
            EventManager.Instance.UpdateObjectiveUI(objectivesText[currentObjectiveIndex ],description);
            Debug.Log("Advanced to new objective: " + objectivesText[currentObjectiveIndex]);
        }
        else
        {
          CompleteEvent();
            if (soundEmitter != null)
                soundEmitter.GetComponent<SoundEmitter>().StopSound();
        }
    }
    

    public void StartObjective()
    {
        if (soundEmitterName != null && playSound)
        {
          soundEmitter = GameObject.Find(soundEmitterName).GetComponent<SoundEmitter>(); 
        }
        EventManager.Instance.UpdateObjectiveUI(objectivesText[currentObjectiveIndex],description);
        if (soundEmitter != null)
        {
            Debug.Log("EmitterFOund");
            soundEmitter.PlayEmitterSound();
            if (soundEmitterName == "Phone")
            {
                soundEmitter.phoneMessage = true;
            }
        }

       
        Debug.Log("CURRENT EVENT IS" + description);
    }

    public void CompleteEvent()
    {
        if (nextEvent != null)
        {
            Debug.Log("Transitioning to next event: " + nextEvent.description);
            EventManager.Instance.StartEvent(nextEvent); // Start the next event if set.
        }
    }
}
