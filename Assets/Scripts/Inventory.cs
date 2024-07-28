using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public List<InventoryItems> items = new List<InventoryItems>();

    public delegate void OnInventoryChanged();
    public event OnInventoryChanged onInventoryChangedCallback;

    public bool AddItem(InventoryItems item)
    {
        if (!items.Contains(item))
        {
            items.Add(item);

            if (onInventoryChangedCallback != null)
                onInventoryChangedCallback.Invoke();
            item.gameObject.SetActive(false);

            return true;
        }
        return false;
    }

    public void RemoveItem(InventoryItems item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);

            if (onInventoryChangedCallback != null)
                onInventoryChangedCallback.Invoke();
        }
    }
}

