using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjects : MonoBehaviour
{
    public InventoryItems item;
    private GameObject player;
    private bool _isWaitingForObject;
    private InventoryUI inventoryUI;

    private void Start()
    {
        inventoryUI = FindObjectOfType<InventoryUI>();
    }

    public void Interact( string objectTag, GameObject playerObject)
    {
        player = playerObject;
        Debug.Log(objectTag);
        if (objectTag == "Collect")
        {
            PickUp();
        }
        if (objectTag == "Give")
        {
            ReceiveItem();
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
