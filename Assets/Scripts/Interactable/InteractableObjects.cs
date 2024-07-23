using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class InteractableObjects : MonoBehaviour
{
    public InventoryItems item;
    private GameObject player;
    public void Interact( string objectTag, GameObject playerObject)
    {
        player = playerObject;
        Debug.Log(objectTag);
        if (objectTag == "Collect")
        {
            PickUp();
        }
    }

    public void PickUp()
    {
        Inventory inventory = player.GetComponent<Inventory>();
        inventory.AddItem(item);
    }

}
