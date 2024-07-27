using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class InteractableObjects : MonoBehaviour
{
    public InventoryItems item;
    public bool objectInteractable = true;
    private GameObject player;
    private bool _isWaitingForObject;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Interact( GameObject currentObject, GameObject playerObject)
    {
        player = playerObject;
        string objectTag = currentObject.tag;
        Debug.Log(objectTag);
        if (objectTag == "Collect")
        {
            PickUp();
        }
        if (objectTag == "Give")
        {
            ReceiveItem();
        }

        if (objectTag == "Phone")
        {
            Debug.Log("is Phone");
            EventManager.Instance.CompleteObjective();
            currentObject.GetComponent<InteractableObjects>().objectInteractable = false;
        }
    }

    private void ReceiveItem()
    {
        inventoryUI.OpenInventoryForInteraction(this);
    }
    
    public void PickUp()
    {
        Inventory inventory = player.GetComponent<Inventory>();
        inventory.AddItem(item);
    }
    public void TryUseItem(InventoryItems selectedItem)
    {
        if (item.usableObjects.Contains(selectedItem))
        {
            Debug.Log($"Successfully used {selectedItem.itemName} on {gameObject.name}");
            OnSuccessfulItemUse();
        }
        else
        {
            Debug.Log($"{selectedItem.itemName} cannot be used on {gameObject.name}");
        }
    }

    private void OnSuccessfulItemUse()
    {
        Debug.Log($"Item used successfully on {gameObject.name}");
    }

}
