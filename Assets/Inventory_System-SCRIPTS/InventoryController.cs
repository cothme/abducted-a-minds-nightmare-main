using System;
using System.Collections.Generic;
using UnityEngine;

//this script is used for controlling the inventory
public class InventoryController : MonoBehaviour
{
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

    void Awake()
    {
        inventoryHighlight = GetComponent<InventoryHighlight>();
    }
    void Update()
    {
        ItemIconDrag();    
        if(Input.GetKeyDown(KeyCode.Q))
        {
            CreateRandomItem();
        }

        if(Input.GetKeyDown(KeyCode.W))
        {
            InsertRandomItem();
        }

        if(Input.GetKeyDown(KeyCode.R))
        {     
            RotateItem();
        }
        if(Input.GetKeyDown(KeyCode.X))
        {
            DeleteItem(selectedItem);
        }
        if(Input.GetKeyDown(KeyCode.D))
        {
            Debug.Log(selectedItem);
            Debug.Log(selectedItemGrid);
            // foreach(ItemData item in items)
            // {
            //     Debug.Log(item);
            // }
        }
        if (selectedItemGrid is null) 
        { 
            inventoryHighlight.Show(false);
            return; 
        }

        HandleHighlight();

        if (Input.GetMouseButtonDown(0))
        {
            LeftMouseButtonPress();
        }
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
            Debug.Log("Success");
        }
    }

    private void RotateItem()
    {
        if(selectedItemGrid == null) { return; }
        Debug.Log(selectedItem.rotated);
        selectedItem.Rotate();
    }

    private void InsertRandomItem()
    {
        if(selectedItemGrid == null) { return; }
        CreateRandomItem();
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

    private void CreateRandomItem()
    {
        InventoryItem inventoryItem = Instantiate(itemPrefab).GetComponent<InventoryItem>();
        selectedItem = inventoryItem;
        rectTransform = inventoryItem.GetComponent<RectTransform>();
        rectTransform.SetParent(canvasTransform);
        
        int selectedItemID = UnityEngine.Random.Range(0,items.Count);
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
        Vector2 position = Input.mousePosition;
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
            rectTransform.position = Input.mousePosition;
        }
    }
}
