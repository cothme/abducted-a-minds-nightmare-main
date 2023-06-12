using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    InventoryController inventoryController;
    public static ItemList Instance;
    private List<int> itemList = new List<int>();
    public List<int>Itemlist { get { return new List<int>(itemList); }}
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();
    public void AddItem(string name)
    {
        switch(name)
        {
            case "Ammo":
            itemList.Add(0);
            GunManager.Instance.Magazine.Add(30);
            GunManager.Instance.UpdateBullets();
            break;
            case "Assault Rifle":
            itemList.Add(1);
            break;
            case "Knife":
            itemList.Add(2);
            break;
            case "Space Mask":
            itemList.Add(3);
            break;
            case "Oxygen Kit":
            itemList.Add(4);
            break;
            case "Handgun":
            itemList.Add(5);
            break;
            case "Shotgun":
            itemList.Add(6);
            break;
            case "Health Kit":
            itemList.Add(7);
            break;
            case "Keycard":
            itemList.Add(8);
            break;
        }
    }
    public void DropItem(string name)
    {
        switch(name)
        {
            case "Ammo":
            itemList.Remove(0);
            GunManager.Instance.Magazine.RemoveAt(0);
            GunManager.Instance.UpdateBullets();
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Assault Rifle":
            itemList.Remove(1);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Knife":
            itemList.Remove(2);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Mask":
            itemList.Remove(3);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Oxygen Kit":
            itemList.Remove(4);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Pistol":
            itemList.Remove(5);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Shotgun":
            itemList.Remove(6);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Health Kit":
            itemList.Remove(7);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Keycard":
            itemList.Remove(8);
            inventoryItems.Remove(inventoryController.selectedItem);
            break;
            default:
            break;
        }
    }    
    private void Awake()
    {
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
