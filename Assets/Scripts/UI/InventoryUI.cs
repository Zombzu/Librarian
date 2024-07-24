using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Transform itemsParent; 
    public GameObject inventorySlotPrefab;

    public GameObject inspectionCanvas;  
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemTitle;
    public Image inspectImage;
    
    private Inventory inventory;
    private int selectedIndex = -1;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.onInventoryChangedCallback += UpdateUI;
        UpdateUI();
        inventoryPanel.SetActive(false);
        
    }

    void UpdateUI()
    {
        // Clear existing items
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }

        // Add new items
        for (int i = 0; i < inventory.items.Count; i++)
        {
            GameObject slot = Instantiate(inventorySlotPrefab, itemsParent);
            Image icon = slot.transform.GetChild(0).GetComponent<Image>();
            icon.sprite = inventory.items[i].itemIcons;

            // Highlight the selected item
            Image highlight = slot.transform.GetChild(1).GetComponent<Image>(); 
            highlight.enabled = (i == selectedIndex);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf); 
        }

        if (inventoryPanel.activeSelf)
        {
            HandleSelection();
        }
        if (inspectionCanvas.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ExitInspection();
        }
    }

    public void ToggleInventory()
    {
        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
    }
    
    void HandleSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedIndex = (selectedIndex + 1) % inventory.items.Count;
            UpdateUI();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedIndex = (selectedIndex - 1 + inventory.items.Count) % inventory.items.Count;
            UpdateUI();
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UseSelectedItem();
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            InspectSelectedItem();
        }
    }

    private void InspectSelectedItem()
    {
        if (selectedIndex >= 0 && selectedIndex < inventory.items.Count)
        {
            if (selectedIndex >= 0 && selectedIndex < inventory.items.Count)
            {
                InventoryItems selectedItem = inventory.items[selectedIndex];
                itemDescriptionText.text = selectedItem.itemDescription;
                itemTitle.text = selectedItem.itemName;
                inspectImage.sprite = selectedItem.itemIcons;
                inspectionCanvas.SetActive(true);
            }
           
        }
    }

    void UseSelectedItem()
    {
        if (selectedIndex >= 0 && selectedIndex < inventory.items.Count)
        {
            InventoryItems selectedItem = inventory.items[selectedIndex];
            Debug.Log("Using item: " + selectedItem.itemName);
        }
    }
    void ExitInspection()
    {
        inspectionCanvas.SetActive(false);
    }
   
}
