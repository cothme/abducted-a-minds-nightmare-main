using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class DataInitialize : MonoBehaviour
{
    [SerializeField] Transform respawnPoint;
    InventoryController inventoryController;
    GameObject player;
    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        // if(PlayerData.Instance.IsSessionSaved == true)
        // {
        //     XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
        //     StreamReader sr = new StreamReader("Abducted Save File");
        //     DataMembers dm = (DataMembers)loadData.Deserialize(sr);
        //     PlayerData.Instance.PlayerPosition = respawnPoint.position;
        //     PlayerData.Instance.PlayerRotation = respawnPoint.rotation;
        //     gameObject.transform.position = PlayerData.Instance.PlayerPosition;
        //     sr.Close();
        // }    
    }
    void Start()
    {
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        // XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
        //     StreamReader sr = new StreamReader("Abducted Save File");
        //     DataMembers dm = (DataMembers)loadData.Deserialize(sr);  
        //     PlayerData.Instance.Stage = dm.level;
        //     PlayerData.Instance.IsSessionSaved = dm.isSessionSaved;
        //     PlayerData.Instance.PlayerHealth = dm.health;
        //     PlayerData.Instance.PlayerOxygen= dm.oxygen;
        //     PlayerData.Instance.PlayerMaxHealth = dm.playerMaxHealth;
        //     GunManager.Instance.WeaponEquipped = dm.weaponEquipped;
        //     GunManager.Instance.BulletsLoaded = dm.bulletsLoaded;
        //     GunManager.Instance.TotalBullets = dm.totalBullets;
        //     PlayerData.Instance.PlayerPosition = dm.position;
        //     PlayerData.Instance.PlayerRotation = dm.rotation;
        //     foreach(int i in dm.itemList)
        //     {
        //         ItemList.Instance.AddItem(i);
        //         inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
        //     }
        //     sr.Close();
        //     ItemList.Instance.AddItem(i);
        //     inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
        // }
        // else
        // {
        //     foreach(int i in ItemList.Instance.Itemlist)
        //     {
        //         ItemList.Instance.AddItem(i);
        //         inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
        //     }
        // } 
        foreach(int i in ItemList.Instance.Itemlist)
        {
            ItemList.Instance.AddItem(i);
            inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
        }
    }
    private void Update()
    {
        // if(positionUpdated)
        // {
        //     gameObject.transform.position = PlayerData.Instance.PlayerPosition;
        //     positionUpdated = false;
        // }
    }
}
