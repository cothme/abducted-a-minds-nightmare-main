using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

//this script is used for controlling the inventory
public class InventoryController : MonoBehaviour
{
    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;
    [SerializeField] InputActionReference inControl;
    [SerializeField] InputActionReference mouseLeftClick;
    [SerializeField] AudioSource generalSound;
    [HideInInspector] private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid { get => selectedItemGrid; 
            set {
                selectedItemGrid = value;
                inventoryHighlight.SetParent(value);
            } }
    public InventoryItem selectedItem;
    InventoryItem overLapItem;
    RectTransform rectTransform;
    InventoryHighlight inventoryHighlight;
    PlayerControls playerControls;
    InputAction rotateButton;
    void Awake()
    {
        playerControls = new PlayerControls();
        inventoryHighlight = GetComponent<InventoryHighlight>();   
    }
    private void OnEnable()
    {
        rotateButton = playerControls.Inventory.RotateItem;
        rotateButton.Enable();
        inControl.action.Enable();
        mouseLeftClick.action.Enable();
    }
    private void OnDisable()
    {
        rotateButton.Disable();
        inControl.action.Disable();
        mouseLeftClick.action.Disable();
    }
    private void Start()
    {
        GameObject playerInteractScript = GameObject.Find("Player");
        PlayerInteractScript playerInteract = playerInteractScript.GetComponent<PlayerInteractScript>();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            RotateItem();
        }
        ItemIconDrag();
        // if(inControl.action.triggered)
        // {
        //     InsertRandomItem();
        // }  
        
        // if(inControl.action.triggered)
        // {
        //     InsertRandomItem(ItemList.Instance.itemList.Last());
        // }
        // if(Input.GetKeyDown(KeyCode.X))
        // {
        //     DeleteItem(selectedItem);
        // }
        // if(Input.GetKeyDown(KeyCode.D))
        // {
        //     Debug.Log(selectedItem);
        //     Debug.Log(selectedItemGrid);
        // }
        if (selectedItemGrid is null) 
        { 
            inventoryHighlight.Show(false);
            return; 
        }
        HandleHighlight();
        if (mouseLeftClick.action.triggered)
        {
            LeftMouseButtonPress();
        }
    }
    public void DeleteItem(InventoryItem item)
    {
        // Destroy(GameObject.FindGameObjectWithTag(tag));
        // selectedItem = null;
        if(selectedItem == null) { Debug.Log("Delete Item Null!!!!"); return; }
        if (selectedItem == item)
        {
            selectedItem = null;
            inventoryHighlight.Show(false);
            Destroy(item.gameObject);
        }
    }
    private void RotateItem()
    {
        if(selectedItemGrid == null || selectedItem == null)
        { 
            return; 
        }
        else 
        { 
            selectedItem.Rotate(); 
        }
    }
    public void InsertRandomItem(int itemID)
    {
        if(selectedItemGrid == null) { return; }
        CreateItem(itemID);
        ItemList.Instance.InventoryItems.Add(selectedItem);
        InventoryItem itemToInsert = selectedItem;
        selectedItem = null;
        InsertItem(itemToInsert);
    }

    private void InsertItem(InventoryItem itemToInsert)
    {
        Vector2Int? posOnGrid = selectedItemGrid.FindSpaceForObject(itemToInsert);

        if(posOnGrid == null) { return; }

        selectedItemGrid.PlaceItem(itemToInsert, posOnGrid.Value.x, posOnGrid.Value.y);
    }

    Vector2Int oldPosition;
    InventoryItem itemToHighlight;

    private void HandleHighlight()
    {
        Vector2Int positionOnGrid = GetTileGridPosition();
        if(oldPosition == positionOnGrid) { return; }
        oldPosition = positionOnGrid;
        if(selectedItem is null)
        {
            try{
                itemToHighlight = selectedItemGrid.GetItem(positionOnGrid.x,positionOnGrid.y);
                if(itemToHighlight != null)
                {
                    inventoryHighlight.Show(true);
                    inventoryHighlight.SetSize(itemToHighlight);
                    inventoryHighlight.SetPosition(selectedItemGrid,itemToHighlight);
                }
                else
                {
                    inventoryHighlight.Show(false);
                }
            }catch(IndexOutOfRangeException)
            {
                return;
            }
        }
        else
        {
            inventoryHighlight.Show(selectedItemGrid.BoundaryCheck(
                positionOnGrid.x,
                positionOnGrid.y,
                selectedItem.WIDTH,
                selectedItem.HEIGHT)
                );
            inventoryHighlight.SetSize(selectedItem);
            inventoryHighlight.SetPosition(selectedItemGrid,selectedItem,positionOnGrid.x,positionOnGrid.y);
        }
    }

    private void CreateItem(int itemID)
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        int selectedItemID = itemID;
        inventoryItem.Set(items[selectedItemID]);
    }

    private void LeftMouseButtonPress()
    {
        try
        {
            Vector2Int tileGridPosition = GetTileGridPosition();
            if (selectedItem is null)
            {
                PickUpItem(tileGridPosition);
            }
            else
            {
                PlaceItem(tileGridPosition);
            }
        }catch(IndexOutOfRangeException)
        {
            return;
        }
    }
    private Vector2Int GetTileGridPosition()
    {
        Vector2 position = Mouse.current.position.ReadValue();
        if (selectedItem != null)
        {
            position.x -= ((selectedItem.WIDTH - 1) * ItemGrid.TileSizeWidth / 2);
            position.y += ((selectedItem.HEIGHT - 1) * ItemGrid.TileSizeHeight / 2);
        }
        return selectedItemGrid.GetTileGridPosition(position);
    }

    public void PlaceItem(Vector2Int tileGridPosition)
    {
        AudioManager.Instance.PlaySound(generalSound,"Inv Select");
        if(selectedItem == null)
        {
            return;
        }
        bool complete = selectedItemGrid.PlaceItem(selectedItem, tileGridPosition.x, tileGridPosition.y, ref overLapItem);
        selectedItem.gameObject.tag = "Untagged";
        if(complete)
        {
            selectedItem = null;
            if(overLapItem != null)
            {
                selectedItem = overLapItem;
                overLapItem = null;
                rectTransform = selectedItem.GetComponent<RectTransform>();
            }
        }
    }

    private void PickUpItem(Vector2Int tileGridPosition)
    {
        AudioManager.Instance.PlaySound(generalSound,"Inv Select");
        selectedItem = selectedItemGrid.PickupItem(tileGridPosition.x, tileGridPosition.y);
        if(selectedItem != null)
        {
            selectedItem.gameObject.tag = "Item";
        }
        else
        {
            return;
        }
        if (selectedItem != null)
        {
            rectTransform = selectedItem.GetComponent<RectTransform>();
        }
        else
        {
            return;
        }
    }
    private void ItemIconDrag()
    {
        if (selectedItem != null)
        {     
            rectTransform.position = Mouse.current.position.ReadValue() + new Vector2(-50,75);
        }
    }
}
