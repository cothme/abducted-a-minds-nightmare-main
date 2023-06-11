using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class GunManager : MonoBehaviour
{
    private static GunManager instance;
    public static GunManager Instance { get { return instance; } }
    [SerializeField] List<WeaponData> weapons;
    InventoryController inventoryController;
    private string weaponEquipped;
    private float bulletsLoaded;
    private float totalBullets;
    private float capacity;
    private float recoil;
    private float reloadTime,attackSpeed;
    private bool canEquipPistol, canEquipRifle, canEquipShotgun,canEquipKnife,canEquipMask;
    float bulletToMinus;
    private List<float> magazine;
    public List<float> Magazine { get => magazine; set => magazine = value; }
    public bool CanEquipPistol { get => canEquipPistol; set => canEquipPistol = value; }
    public bool CanEquipRifle { get => canEquipRifle; set => canEquipRifle = value; }
    public bool CanEquipShotgun { get => canEquipShotgun; set => canEquipShotgun = value; }
    public bool CanEquipKnife { get => canEquipKnife; set => canEquipKnife = value; }
    public string WeaponEquipped { get => weaponEquipped; set => weaponEquipped = value; }
    public float BulletsLoaded { get => bulletsLoaded; set => bulletsLoaded = value; }
    public float ReloadTime { get => reloadTime; set => reloadTime = value; }
    public float AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public List<WeaponData> Weapons { get => weapons; set => weapons = value; }
    public float TotalBullets { get => totalBullets; set => totalBullets = value; }
    public bool CanEquipMask { get => canEquipMask; set => canEquipMask = value; }

    private void Initialize()
    {
        magazine = new List<float> {  };
    }
    private void Update()
    {
        CheckForWeapon();
    }
    public void shootInGunManager()
    {
        try
        {
            if(BulletsLoaded < 0)
                {
                    Debug.Log("No Bullets!");
                    return;
                }
                else
                {
                    BulletsLoaded -= 1;
                    magazine[0] -= 1;
                }
            if(magazine.Count > 0)
            {
                if(magazine[0] <= 1)
                {   
                    magazine.RemoveAt(0);
                    foreach(InventoryItem i in ItemList.Instance.inventoryItems)
                    {
                        if(i.itemData.name == "Ammo")
                        {
                            ItemList.Instance.inventoryItems.Remove(i);
                            inventoryController.selectedItem = i;
                            inventoryController.DeleteItem(inventoryController.selectedItem);
                            break;
                        }
                    }
                }
            }
            else
            {
                return;
            }
        }
        catch (ArgumentOutOfRangeException)
        {
            return;
        }
    }
    public void reloadInGunManager()
    {
        if(bulletsLoaded == capacity)
        {
            return;
        }
        else
        {
            StartCoroutine(ReloadCoroutine()); 
        }
    }
    public void UpdateBullets()
    {   
        TotalBullets = magazine.Sum() - bulletsLoaded;
    }
    private IEnumerator ReloadCoroutine()
    {        
        if(!PlayerState.Instance.Reloading)
        {   
            PlayerState.Instance.Reloading = true;
            float remainingTime = ReloadTime;
            while (remainingTime > 0)
            {
                yield return new WaitForSeconds(0.1f);
                remainingTime -= 0.1f; 
            }       
            float reloadAmount = capacity - bulletsLoaded;
            reloadAmount = (TotalBullets - reloadAmount) >= 0 ? reloadAmount : TotalBullets;
            bulletsLoaded += reloadAmount;
            TotalBullets -= reloadAmount;  
            PlayerState.Instance.Reloading = false;  
        }
    }
    public void CheckForWeapon()
    {
        if(ItemList.Instance.Itemlist.Contains(1))
        {
            CanEquipRifle = true;
        }
        else
        {
            CanEquipRifle = false;
            
        }
        if(ItemList.Instance.Itemlist.Contains(5))
        {
            CanEquipPistol = true;
        }
        else
        {
            CanEquipPistol = false;
        }
        if(ItemList.Instance.Itemlist.Contains(6))
        {
            CanEquipShotgun = true;
        }
        else
        {
            CanEquipShotgun = false;
        }
        if(ItemList.Instance.Itemlist.Contains(2))
        {
            CanEquipKnife = true;
        }
        else
        {
            CanEquipKnife = false;
        }
        if(ItemList.Instance.Itemlist.Contains(3))
        {
            CanEquipMask = true;
        }
        else
        {
            CanEquipMask = false;
        }
    }
    public void SetWeaponChanges()
   {
        switch (weaponEquipped)
        {
            case "Rifle":
                ReloadTime = Weapons[0].reloadSpeed;
                AttackSpeed = Weapons[0].attackSpeed;
                capacity = Weapons[0].capacity;
                recoil = Weapons[0].weaponRecoil;
                if(bulletsLoaded > capacity)
                {
                    float bulletsStored = bulletsLoaded - capacity;
                    bulletsLoaded = capacity;
                    TotalBullets += bulletsStored;
                }
                break;
            case "Shotgun":
                ReloadTime = Weapons[1].reloadSpeed;
                AttackSpeed = Weapons[1].attackSpeed;
                capacity = Weapons[1].capacity;
                recoil = Weapons[1].weaponRecoil;
                if(bulletsLoaded > capacity)
                {
                    float bulletsStored = bulletsLoaded - capacity;
                    bulletsLoaded = capacity;
                    TotalBullets += bulletsStored;
                }
                break;
            case "Pistol":
                ReloadTime = Weapons[2].reloadSpeed;
                AttackSpeed = Weapons[2].attackSpeed;
                capacity = Weapons[2].capacity;
                recoil = Weapons[2].weaponRecoil;
                if(bulletsLoaded > capacity)
                {
                    float bulletsStored = bulletsLoaded - capacity;
                    bulletsLoaded = capacity;
                    TotalBullets += bulletsStored;
                }
                break;
            case "Knife":
                ReloadTime = Weapons[3].reloadSpeed;
                AttackSpeed = Weapons[3].attackSpeed;
                capacity = Weapons[3].capacity;
                recoil = Weapons[3].weaponRecoil;
                break;
            default:
                break;
        }
   }
    private void Awake()
    {
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
        Initialize();
    }
}
