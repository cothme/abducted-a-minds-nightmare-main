using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    public ItemData itemData;
    public int onGridPositionX;
    public int onGridPositionY;

public int HEIGHT
    {
        get{
            if (rotated == false)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }
    public int WIDTH
    {
        get{
            if (rotated == true)
            {
                return itemData.height;
            }
            return itemData.width;
        }
    }
    public bool rotated = false;

    internal void Rotate()
    {
        if(itemData.width == itemData.height) { return; }
        rotated = !rotated;

        Debug.Log(WIDTH + " " + HEIGHT);

        RectTransform rectTransform = GetComponent<RectTransform>();
        rectTransform.transform.rotation = Quaternion.Euler(0,0,rotated == true ?  90f : 0f);
    }

    internal void Set(ItemData itemData)
    {
        this.itemData = itemData;

        GetComponent<Image>().sprite = itemData.itemIcon;

        Vector2 size = new Vector2();
        size.x = itemData.width * ItemGrid.TileSizeWidth;
        size.y = itemData.height * ItemGrid.TileSizeHeight;
        GetComponent<RectTransform>().sizeDelta = size;
    }
}
