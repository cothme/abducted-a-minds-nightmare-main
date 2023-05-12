using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

//this script is used for controlling the inventory
public class InventoryController : MonoBehaviour
{
    [SerializeField] InputActionReference inControl;
    [HideInInspector] private ItemGrid selectedItemGrid;
    public ItemGrid SelectedItemGrid { get => selectedItemGrid; 
            set {
                selectedItemGrid = value;
                inventoryHighlight.SetParent(value);
            } }
    InventoryItem selectedItem;
    InventoryItem overLapItem;
    RectTransform rectTransform;

    [SerializeField] List<ItemData> items;
    [SerializeField] GameObject itemPrefab;
    [SerializeField] Transform canvasTransform;

    InventoryHighlight inventoryHighlight;
    private void OnEnable()
    {
        inControl.action.Enable();
    }
    private void OnDisable()
    {
        inControl.action.Disable();
    }
    void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();   
    }
    private void Start()
    {
        GameObject playerInteractScript = GameObject.Find("Player");
        PlayerInteractScript playerInteract = playerInteractScript.GetComponent<PlayerInteractScript>();
    }
    void Update()
    {
        ItemIconDrag();
        // if(inControl.action.triggered)
        // {
        //     InsertRandomItem();
        // }  
        
        // if(inControl.action.triggered)
        // {
        //     InsertRandomItem(ItemList.Instance.itemList.Last());
        // }
        // if(Input.GetKeyDown(KeyCode.R))
        // {     
        //     RotateItem();
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
        // if (Input.GetMouseButtonDown(0))
        // {
        //     LeftMouseButtonPress();
        // }
    }
    public void DeleteItem(InventoryItem item)
    {
        // Destroy(GameObject.FindGameObjectWithTag(tag));
        // selectedItem = null;
        if(selectedItem == null) { return; }
        if (selectedItem == item)
        {
            selectedItem = null;
            inventoryHighlight.Show(false);
        }
        if(items.Contains(item.itemData))
        {
            for(int x = 0; x < item.onGridPositionX + item.WIDTH; x++)
            {
                for(int y = 0; y < item.HEIGHT; y++)
                {
                    selectedItemGrid.inventoryItemSlot[x, y] = null;
                }
            }
            Destroy(item.gameObject);
        }
    }

    private void RotateItem()
    {
        if(selectedItemGrid == null) { return; }
        Debug.Log(selectedItem.rotated);
        selectedItem.Rotate();
    }

    public void InsertRandomItem(int itemID)
    {
        if(selectedItemGrid == null) { return; }
        CreateItem(itemID);
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
        Vector2Int tileGridPosition = GetTileGridPosition();

        if (selectedItem is null)
        {
            PickUpItem(tileGridPosition);
        }
        else
        {
            PlaceItem(tileGridPosition);
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
            rectTransform.position = Mouse.current.position.ReadValue();
        }
    }
}
