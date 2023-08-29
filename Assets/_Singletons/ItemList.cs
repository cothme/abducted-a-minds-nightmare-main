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
    public List<InventoryItem> InventoryItems { get => inventoryItems; set => inventoryItems = value; }
    private List<InventoryItem> inventoryItems = new List<InventoryItem>();
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
            case "Flashlight":
            itemList.Add(9);
            break;
            case "UV Flashlight":
            itemList.Add(10);
            break;
        }
    }
    public void AddItem(int id)
    {
        switch(id)
        {
            case 0:
            itemList.Add(0);
            break;
            case 1:
            itemList.Add(1);
            break;
            case 2:
            itemList.Add(2);
            break;
            case 3:
            itemList.Add(3);
            break;
            case 4:
            itemList.Add(4);
            break;
            case 5:
            itemList.Add(5);
            break;
            case 6:
            itemList.Add(6);
            break;
            case 7:
            itemList.Add(7);
            break;
            case 8:
            itemList.Add(8);
            break;
            case 9:
            itemList.Add(9);
            break;
            case 10:
            itemList.Add(10);
            break;
        }
    }
    public void DropItem(string name)
    {
        switch(name)
        {
            case "Ammo1":
            itemList.Remove(0);
            break;
            case "Ammo":
            itemList.Remove(0);
            InventoryItems.Remove(inventoryController.selectedItem);
            GunManager.Instance.Magazine.RemoveAt(0);
            GunManager.Instance.UpdateBullets();
            break;
            case "Assault Rifle":
            itemList.Remove(1);
            InventoryItems.Remove(inventoryController.selectedItem);
            GunManager.Instance.WeaponEquipped = null;
            break;
            case "Knife":
            itemList.Remove(2);
            InventoryItems.Remove(inventoryController.selectedItem);
            GunManager.Instance.WeaponEquipped = null;
            break;
            case "Mask":
            itemList.Remove(3);
            InventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Oxygen Kit":
            itemList.Remove(4);
            InventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Pistol":
            itemList.Remove(5);
            InventoryItems.Remove(inventoryController.selectedItem);
            GunManager.Instance.WeaponEquipped = null;
            break;
            case "Shotgun":
            itemList.Remove(6);
            InventoryItems.Remove(inventoryController.selectedItem);
            GunManager.Instance.WeaponEquipped = null;
            break;
            case "Health Kit":
            itemList.Remove(7);
            InventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "KeyCard":
            itemList.Remove(8);
            InventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "Flashlight":
            itemList.Remove(9);
            InventoryItems.Remove(inventoryController.selectedItem);
            break;
            case "UV Flashlight":
            itemList.Remove(10);
            InventoryItems.Remove(inventoryController.selectedItem);
            break;
            default:
            break;
        }
    }    
    public void ClearItemList()
    {
        itemList.Clear();
    }
    public void ClearInventoryItems()
    {
        inventoryItems.Clear();
    }
    public void RemoveKeyCard()
    {
        foreach(InventoryItem i in InventoryItems)
            {
                if(i.itemData.name == "KeyCard")
                {
                    InventoryItems.Remove(i);
                }
            }
    }
    private void Update()
    {
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        if(Input.GetKeyDown(KeyCode.T))
        {
            // foreach(InventoryItem i in InventoryItems)
            // {
            //     Debug.Log(i.itemData.name);
            // }
            foreach(int i in Itemlist)
            {
                Debug.Log(i);
            }
            foreach(InventoryItem i in InventoryItems)
            {
                Debug.Log(i.itemData.name);
            }
            foreach(float i in GunManager.Instance.Magazine)
            {
                Debug.Log(i);
            }
        }
    }
    private void Awake()
    {
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
