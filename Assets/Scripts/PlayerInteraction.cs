using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionRange = 2f; 
    public LayerMask interactableLayer;

    public float outlineWidth = 4f;
    //public GameObject interactionPrompt; 

    public Camera playerCamera;
    private Outline _currentObject;
    void Start()
    {
       // _playerCamera = Camera.main;
       // interactionPrompt.SetActive(false);
    }

    void Update()
    {
        CheckForInteractable();
    }

    void CheckForInteractable()
    {
       // Ray ray = _playerCamera.GetComponent<Camera>().ViewportPointToRay(new Vector3(0.5f, 0,5f));
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactionRange, interactableLayer))
        {
            
           InteractableObjects interactable = hit.collider.GetComponent<InteractableObjects>();
            if (interactable != null && interactable.objectInteractable)
            {
              //  interactionPrompt.SetActive(true);
              _currentObject = interactable.gameObject.GetComponent<Outline>(); 
              _currentObject.OutlineWidth = 3f;
                if (Input.GetKeyDown(KeyCode.E))
                {
                    interactable.Interact(interactable.gameObject, this.gameObject);
                }
            }
        }
        else
        {
           // interactionPrompt.SetActive(false);
           if (_currentObject != null)
           {
               _currentObject.OutlineWidth = 0f;
           }
           
        }
    }
}
