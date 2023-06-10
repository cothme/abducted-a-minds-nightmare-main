using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    Vector3 playerPosition;
    float playerHealth;
    float playerOxygen;
    string weaponEquipped;
    float bulletsLoaded;
    List<int> inventoryItems;
    float level;
    
    void Update()
    {
        weaponEquipped = GunManager.Instance.WeaponEquipped;
        bulletsLoaded = GunManager.Instance.BulletsLoaded;
        inventoryItems = ItemList.Instance.Itemlist;
        if(Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(weaponEquipped);
            Debug.Log(bulletsLoaded);
            Debug.Log(inventoryItems.Count);
        }
    }
}
