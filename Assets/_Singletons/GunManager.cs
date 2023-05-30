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
    [SerializeField] TextMeshProUGUI magazineCountText;
    [SerializeField] TextMeshProUGUI totalBulletsCountText;
    [SerializeField] TextMeshProUGUI bulletsCountText;
    [SerializeField] TextMeshProUGUI statusIndicator;
    [SerializeField] TextMeshProUGUI weaponIndicator;
    InventoryController inventoryController;
    private string weaponEquipped;
    private float bulletsLoaded;
    private float totalBullets;
    private float capacity;
    public float reloadTime,attackSpeed;
    private bool canEquipPistol, canEquipRifle, canEquipShotgun,canEquipKnife;
    float bulletToMinus;
    private List<float> magazine;
    public List<float> Magazine { get => magazine; set => magazine = value; }
    public bool CanEquipPistol { get => canEquipPistol; set => canEquipPistol = value; }
    public bool CanEquipRifle { get => canEquipRifle; set => canEquipRifle = value; }
    public bool CanEquipShotgun { get => canEquipShotgun; set => canEquipShotgun = value; }
    public bool CanEquipKnife { get => canEquipKnife; set => canEquipKnife = value; }
    public string WeaponEquipped { get => weaponEquipped; set => weaponEquipped = value; }
    public float BulletsLoaded { get => bulletsLoaded; set => bulletsLoaded = value; }

    private void Initialize()
    {
        magazine = new List<float> {  };
    }
    private void Update()
    {
        weaponIndicator.text = WeaponEquipped;
        totalBulletsCountText.text = totalBullets.ToString();
        bulletsCountText.text = BulletsLoaded.ToString();
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
        totalBullets = magazine.Sum() - bulletsLoaded;
    }
    private IEnumerator ReloadCoroutine()
    {
        if(!PlayerState.Instance.Reloading)
        {
            PlayerState.Instance.Reloading = true;  
            float remainingTime = reloadTime;
            while (remainingTime > 0)
            {
                statusIndicator.text = "Reloading: " + remainingTime.ToString("F1");
                yield return new WaitForSeconds(0.1f);
                remainingTime -= 0.1f; 
            }       
            float reloadAmount = capacity - bulletsLoaded;
            reloadAmount = (totalBullets - reloadAmount) >= 0 ? reloadAmount : totalBullets;
            bulletsLoaded += reloadAmount;
            totalBullets -= reloadAmount;  
            PlayerState.Instance.Reloading = false;  
        }
    }
    public void CheckForWeapon()
    {
        foreach (float item in ItemList.Instance.Itemlist)
        {
            switch (item)
            {
                case 1:
                    CanEquipRifle = true;
                    break;
                case 5:
                    CanEquipPistol = true;
                    break;
                case 6:
                    CanEquipShotgun = true;
                    break;
                case 2:
                    CanEquipKnife = true;
                    break;
                default:
                    break;
            }
        }
    }
    public void SetWeaponChanges()
   {
        switch (weaponEquipped)
        {
            case "Rifle":
                reloadTime = weapons[0].reloadSpeed;
                attackSpeed = weapons[0].attackSpeed;
                capacity = weapons[0].capacity;
                if(bulletsLoaded > capacity)
                {
                    float bulletsStored = bulletsLoaded - capacity;
                    bulletsLoaded = capacity;
                    totalBullets += bulletsStored;
                }
                break;
            case "Shotgun":
                reloadTime = weapons[1].reloadSpeed;
                attackSpeed = weapons[1].attackSpeed;
                capacity = weapons[1].capacity;
                if(bulletsLoaded > capacity)
                {
                    float bulletsStored = bulletsLoaded - capacity;
                    bulletsLoaded = capacity;
                    totalBullets += bulletsStored;
                }
                break;
            case "Pistol":
                reloadTime = weapons[2].reloadSpeed;
                attackSpeed = weapons[2].attackSpeed;
                capacity = weapons[2].capacity;
                if(bulletsLoaded > capacity)
                {
                    float bulletsStored = bulletsLoaded - capacity;
                    bulletsLoaded = capacity;
                    totalBullets += bulletsStored;
                }
                break;
            case "Knife":
                reloadTime = weapons[3].reloadSpeed;
                attackSpeed = weapons[3].attackSpeed;
                capacity = weapons[3].capacity;
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
