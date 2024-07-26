using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItems : MonoBehaviour
{
    public string itemName;
    public Sprite itemIcons;
    public string itemDescription;
    public bool isUsable;
    public bool canTakeItems;
    public List<InventoryItems> usableObjects;
}
