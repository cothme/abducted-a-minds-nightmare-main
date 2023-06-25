using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerData : MonoBehaviour
{
    private static PlayerData instance;
    Vector3 playerPosition;
    Quaternion playerRotation;
    float playerMaxHealth = 50f;
    float playerHealth = 25f;
    float playerOxygen = 25f;
    float playerMaxOxygen = 50f;
    float gemsCollected = 0f;
    float volume;
    float stage;
    bool isSessionSaved = false;
    public static PlayerData Instance { get { return instance; } }
    public float PlayerHealth { get => playerHealth; set => playerHealth = value; }
    public float PlayerOxygen { get => playerOxygen; set => playerOxygen = value; }
    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public float GemsCollected { get => gemsCollected; set => gemsCollected = value; }
    public float Stage { get => stage; set => stage = value; }
    public Quaternion PlayerRotation { get => playerRotation; set => playerRotation = value; }
    public bool IsSessionSaved { get => isSessionSaved; set => isSessionSaved = value; }
    public float Volume { get => volume; set => volume = value; }
    public float PlayerMaxHealth { get => playerMaxHealth; set => playerMaxHealth = value; }
    public float PlayerMaxOxygen { get => playerMaxOxygen; set => playerMaxOxygen = value; }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            // Debug.Log("Inventory Items: "  + ItemList.Instance.InventoryItems.Count);
            // Debug.Log("Item List: "  + ItemList.Instance.Itemlist.Count);
            // Debug.Log("Can fire: " + GunManager.Instance.WeaponEquipped);
            // Debug.Log("Can equip pistol: " + GunManager.Instance.CanEquipPistol);
            // foreach(int i in ItemList.Instance.Itemlist)
            // {
            //     Debug.Log(i);
            // }
            Debug.Log(PlayerData.Instance.Stage);
            Debug.Log(PlayerData.Instance.PlayerHealth);
            Debug.Log(PlayerData.Instance.IsSessionSaved);
            Debug.Log(PlayerData.Instance.PlayerPosition);
        }
        Stage = Level();
    }
    public float Level()
    {
        switch(SceneManager.GetActiveScene().name)
        {
            case "level 1":
            return 1;
            case "level 2":
            return 2;
            case "level 3":
            return 3;
            case "level 4":
            return 4;
            default:
            return 0;
        }
    }
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
