using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f; 
    public LayerMask interactableLayer;

    public float outlineWidth = 4f;
    //public GameObject interactionPrompt; 

    private Camera playerCamera;
    private Outline currentObject;
    void Start()
    {
        playerCamera = Camera.main;
       // interactionPrompt.SetActive(false);
    }

    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            
           InteractableObjects interactable = hit.collider.GetComponent<InteractableObjects>();
            if (interactable != null)
            {
              //  interactionPrompt.SetActive(true);
              currentObject = interactable.gameObject.GetComponent<Outline>(); 
              currentObject.OutlineWidth = 3f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(interactable.gameObject.tag);
                }
            }
        }
        else
        {
           // interactionPrompt.SetActive(false);
           if (currentObject != null)
           {
               currentObject.OutlineWidth = 0f;
           }
           
        }
    }
}
