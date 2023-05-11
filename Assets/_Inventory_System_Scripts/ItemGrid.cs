using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGrid : MonoBehaviour
{
    public const float TileSizeWidth = 64;
    public const float TileSizeHeight = 64;
    public InventoryItem[,] inventoryItemSlot;

    RectTransform rectTransform;
    [SerializeField] int gridSizeWidth;
    [SerializeField] int gridSizeHeight;
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Init(gridSizeWidth, gridSizeHeight);
    }

    private void Init(int width, int height)
    {
        inventoryItemSlot = new InventoryItem[width, height];
        Vector2 size = new Vector2(width * TileSizeWidth, height * TileSizeHeight);
        rectTransform.sizeDelta = size;
    }

    Vector2 positionOnTheGrid = new Vector2();
    Vector2Int tileGridPosition = new Vector2Int();

    public Vector2Int GetTileGridPosition(Vector2 mousePosition)
    {
        positionOnTheGrid.x = mousePosition.x - rectTransform.position.x;
        positionOnTheGrid.y = rectTransform.position.y - mousePosition.y;

        tileGridPosition.x = (int)(positionOnTheGrid.x / TileSizeWidth);
        tileGridPosition.y = (int)(positionOnTheGrid.y / TileSizeHeight);

        return tileGridPosition;
    }

    public bool PlaceItem(InventoryItem inventoryItem, int posX, int posY, ref InventoryItem overLapItem)
    {
        if (BoundaryCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT) == false)
        {
            overLapItem = null;
            return false;
        }

        if (OverLapCheck(posX, posY, inventoryItem.WIDTH, inventoryItem.HEIGHT, ref overLapItem) == false)
        {
            return false;
        }

        if (overLapItem != null)
        {
            ClearGridReference(overLapItem);
        }

        PlaceItem(inventoryItem, posX, posY);

        return true;
    }

    public void PlaceItem(InventoryItem inventoryItem, int posX, int posY)
    {
        RectTransform itemRectTransform = inventoryItem.GetComponent<RectTransform>();
        itemRectTransform.SetParent(rectTransform);

        for (int x = 0; x < inventoryItem.WIDTH; x++)
        {
            for (int y = 0; y < inventoryItem.HEIGHT; y++)
            {
                inventoryItemSlot[posX + x, posY + y] = inventoryItem;
            }
        }

        inventoryItem.onGridPositionX = posX;
        inventoryItem.onGridPositionY = posY;

        Vector2 position = CalculatePositionOnGridItem(inventoryItem, posX, posY);
        itemRectTransform.localPosition = position;
        Debug.Log(itemRectTransform.localPosition);
    }

    public Vector2 CalculatePositionOnGrid(InventoryItem inventoryItem,int posX, int posY)
    {
        Vector2 position = new Vector2();
        position.x = ((posX * (TileSizeWidth + TileSizeWidth) * inventoryItem.WIDTH) / 2) / inventoryItem.WIDTH;
        position.y = -(((posY * (TileSizeHeight + TileSizeHeight) * inventoryItem.HEIGHT / 2) / inventoryItem.HEIGHT));
        return position;
    }
    public Vector2 CalculatePositionOnGridItem(InventoryItem inventoryItem,int posX, int posY)
    {
        Vector2 position = new Vector2();
        if(!inventoryItem.rotated)
        {
            position.x = ((posX * (TileSizeWidth + TileSizeWidth) * inventoryItem.WIDTH) / 2) / inventoryItem.WIDTH;
            position.y = -(((posY * (TileSizeHeight + TileSizeHeight) * inventoryItem.HEIGHT / 2) / inventoryItem.HEIGHT));     
        }
        else
        {
            position.x = ((posX * (TileSizeWidth + TileSizeWidth) * inventoryItem.WIDTH) / 2) / inventoryItem.WIDTH;
            position.y = -(((posY * (TileSizeHeight + TileSizeHeight) * inventoryItem.HEIGHT / 2) / inventoryItem.HEIGHT) + inventoryItem.HEIGHT * TileSizeHeight);     
        }
        return position;  
    }
    private bool OverLapCheck(int posX, int posY, int width, int height, ref InventoryItem overLapItem)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX + x, posY + y] != null)
                {
                    if(overLapItem is null)
                    {
                        overLapItem = inventoryItemSlot[posX + x, posY + y];
                    }
                    else
                    {
                        if(overLapItem != inventoryItemSlot[posX + x, posY + y])
                        {
                            return false;
                        }
                    } 
                }
            }
        }
        return true;
    }

    public InventoryItem PickupItem(int x, int y)
    {
        InventoryItem toReturn = inventoryItemSlot[x, y];

        if (toReturn is null) { return null; }

        ClearGridReference(toReturn);

        return toReturn;
    }

    private void ClearGridReference(InventoryItem item)
    {
        for (int ix = 0; ix < item.WIDTH; ix++)
        {
            for (int iy = 0; iy < item.HEIGHT; iy++)
            {
                inventoryItemSlot[item.onGridPositionX + ix, item.onGridPositionY + iy] = null;
            }
        }
    }

    bool PositionCheck(int posX, int posY)
    {
        if(posX < 0 || posY < 0){ return false; }  

        if(posX >= gridSizeWidth || posY >= gridSizeHeight){ return false; }

        return true;
    }

    public bool BoundaryCheck(int posX, int posY, int width, int height)
    {
        if(PositionCheck(posX,posY) == false) { return false; }

        posX += width-1;    
        posY += height-1;

        if(PositionCheck(posX,posY) == false) { return false; }    

        return true;
    }

    public InventoryItem GetItem(int x, int y)
    {
        return inventoryItemSlot[x,y];
    }

    public Vector2Int? FindSpaceForObject(InventoryItem itemToInsert)
    {
        int height = gridSizeHeight - itemToInsert.HEIGHT + 1;
        int width = gridSizeWidth - itemToInsert.WIDTH + 1;

        for(int y = 0; y < height; y++)
        {
            for(int x = 0; x < width; x++)
            {
                if(CheckAvailableSpace(x,y, itemToInsert.WIDTH, itemToInsert.HEIGHT))
                {
                    return new Vector2Int(x,y);
                }
            }
        }
        return null;      
    }
    private bool CheckAvailableSpace(int posX, int posY, int width, int height)
    {
        for(int x = 0; x < width; x++)
        {
            for(int y = 0; y < height; y++)
            {
                if(inventoryItemSlot[posX + x, posY + y] != null)
                {
                    return false;
                }
            } 
        }
        return true;
    }
}
