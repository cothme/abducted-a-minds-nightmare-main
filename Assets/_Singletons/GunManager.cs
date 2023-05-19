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
    private int bulletsLoaded;
    private int totalBullets;
    private int maxBullets = 30;
    public float reloadTime;
    private bool canEquipPistol, canEquipRifle, canEquipShotgun,canEquipKnife;
    int bulletToMinus;
    private List<int> magazine;
    public List<int> Magazine { get => magazine; set => magazine = value; }
    public bool CanEquipPistol { get => canEquipPistol; set => canEquipPistol = value; }
    public bool CanEquipRifle { get => canEquipRifle; set => canEquipRifle = value; }
    public bool CanEquipShotgun { get => canEquipShotgun; set => canEquipShotgun = value; }
    public bool CanEquipKnife { get => canEquipKnife; set => canEquipKnife = value; }
    public string WeaponEquipped { get => weaponEquipped; set => weaponEquipped = value; }
    public int BulletsLoaded { get => bulletsLoaded; set => bulletsLoaded = value; }

    private void Initialize()
    {
        magazine = new List<int> {  };
    }
    private void Update()
    {
        weaponIndicator.text = WeaponEquipped;
        totalBulletsCountText.text = totalBullets.ToString();
        bulletsCountText.text = BulletsLoaded.ToString();
        Debug.Log(magazine.Count + "      " + magazine[0]);
        Debug.Log(inventoryController.selectedItem);
    }
    public void Shoot()
    {
        if(magazine.Count > 0)
        {
            if(magazine[0] <= 0)
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
            Debug.Log("No magazines!!");
        }
        if(BulletsLoaded <= 0)
        {
            return;
        }
        else
        {
            BulletsLoaded -= 1;
            magazine[0] -= 1;
        }
    }
    public void Reload()
    {
        StartCoroutine(ReloadCoroutine()); 
    }
    public void UpdateBullets()
    {   
        totalBullets = magazine.Sum() - bulletsLoaded;
    }
    private IEnumerator ReloadCoroutine()
    {
        float remainingTime = reloadTime;
        while (remainingTime > 0)
        {
            statusIndicator.text = "Reloading: " + remainingTime.ToString("F1");
            yield return new WaitForSeconds(0.1f);
            remainingTime -= 0.1f; 
        }       
        int reloadAmount = maxBullets - bulletsLoaded;
        reloadAmount = (totalBullets - reloadAmount) >= 0 ? reloadAmount : totalBullets;
        bulletsLoaded += reloadAmount;
        totalBullets -= reloadAmount;    
    }
    public void CheckForWeapon()
    {
        foreach (int item in ItemList.Instance.Itemlist)
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
                break;
            case "Shotgun":
                reloadTime = weapons[1].reloadSpeed;
                break;
            case "Pistol":
                reloadTime = weapons[2].reloadSpeed;
                break;
            case "Knife":
                reloadTime = weapons[3].reloadSpeed;
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
