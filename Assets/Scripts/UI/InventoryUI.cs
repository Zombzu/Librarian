using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using PolyAndCode.UI;


public class InventoryUI : MonoBehaviour
{
   public GameObject inventoryPanel;
    public Transform itemsParent; 
    public GameObject inventorySlotPrefab;

    public GameObject inspectionCanvas;  
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemTitle;
    public Image inspectImage;
    public ScrollRect scrollRect;
    public float scrollSpeed;
    
    private Inventory inventory;
    private int selectedIndex = -1;
    private bool isSelectingForInteraction = false;
    private InteractableObjects currentInteractableObject;

    void Start()
    {
        inventory = FindObjectOfType<Inventory>();
        inventory.onInventoryChangedCallback += UpdateUI;
        UpdateUI();
        inventoryPanel.SetActive(false);
    }
    
    public void OpenInventoryForInteraction(InteractableObjects interactableObject)
    {
        isSelectingForInteraction = true;
        currentInteractableObject = interactableObject;
        inventoryPanel.SetActive(true);
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
            if (selectedIndex < inventory.items.Count - 1)  
            {
                scrollRect.horizontalNormalizedPosition += scrollSpeed * Time.deltaTime;
                selectedIndex++;
                StartCoroutine( ScrollToItem(selectedIndex)); 
                UpdateUI();  
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (selectedIndex > 0)  
            {
                scrollRect.horizontalNormalizedPosition -= scrollSpeed * Time.deltaTime;
                selectedIndex--;  
               StartCoroutine( ScrollToItem(selectedIndex));  
                UpdateUI();  
            }
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            UseSelectedItem();
            if (isSelectingForInteraction)
            {
                UseSelectedItemForInteraction();
            }
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
            InventoryItems selectedItem = inventory.items[selectedIndex];
            itemDescriptionText.text = selectedItem.itemDescription;
            itemTitle.text = selectedItem.itemName;
            inspectImage.sprite = selectedItem.itemIcons;
            inspectionCanvas.SetActive(true);
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
    IEnumerator ScrollToItem(int itemIndex)
    {
        
        Debug.Log(scrollRect.horizontalNormalizedPosition.ToString());
        Canvas.ForceUpdateCanvases(); 
        Debug.Log("should scroll");
        float normalizedPosition = (float)itemIndex / (inventory.items.Count - 1);
        Debug.Log("Normalized Pos: " + normalizedPosition.ToString());
        yield return new WaitForEndOfFrame();
       scrollRect.movementType = ScrollRect.MovementType.Elastic;
       scrollRect.elasticity = 0f;
       scrollRect.horizontalNormalizedPosition = normalizedPosition;
        Debug.Log(scrollRect.horizontalNormalizedPosition.ToString());
        yield return new WaitForEndOfFrame();
         scrollRect.movementType = ScrollRect.MovementType.Clamped;
         yield return new WaitForEndOfFrame();
    }
    
    void ExitInspection()
    {
        inspectionCanvas.SetActive(false);
    }

    private void UseSelectedItemForInteraction()
    {
        if (selectedIndex >= 0 && selectedIndex < inventory.items.Count)
        {
            InventoryItems selectedItem = inventory.items[selectedIndex];
            currentInteractableObject.TryUseItem(selectedItem);
            inventoryPanel.SetActive(false);
            isSelectingForInteraction = false;
        }
    }
}