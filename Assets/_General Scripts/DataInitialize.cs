using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

public class DataInitialize : MonoBehaviour
{
    InventoryController inventoryController;
    void Awake()
    {
        if(PlayerData.Instance.IsSessionSaved == true)
        {
            XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
            StreamReader sr = new StreamReader("Abducted Save File");
            DataMembers dm = (DataMembers)loadData.Deserialize(sr);
            PlayerData.Instance.PlayerPosition = dm.position;
            PlayerData.Instance.PlayerRotation = dm.rotation;
            sr.Close();
        }    
    }
    void Start()
    {
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        if(PlayerData.Instance.IsSessionSaved == true)
        {
            XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
            StreamReader sr = new StreamReader("Abducted Save File");
            DataMembers dm = (DataMembers)loadData.Deserialize(sr);  
            PlayerData.Instance.Stage = dm.level;
            PlayerData.Instance.IsSessionSaved = dm.isSessionSaved;
            PlayerData.Instance.PlayerHealth = dm.health;
            PlayerData.Instance.PlayerOxygen= dm.oxygen;
            GunManager.Instance.WeaponEquipped = dm.weaponEquipped;
            inventoryController.InsertRandomItem(1);
            GunManager.Instance.BulletsLoaded = dm.bulletsLoaded;
            GunManager.Instance.TotalBullets = dm.totalBullets;
            gameObject.transform.position = PlayerData.Instance.PlayerPosition;
            foreach(int i in dm.itemList)
            {
                ItemList.Instance.AddItem(i);
                inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
            }
            sr.Close();
        }   
    }
}
