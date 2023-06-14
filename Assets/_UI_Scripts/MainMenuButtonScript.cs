using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.EventSystems;
using System;
using System.IO;
using System.Xml.Serialization;
using UnityEngine.SceneManagement;

public class MainMenuButtonScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image highlightImage;
    public TextMeshProUGUI text;
    private Vector3 originalPosition,targetPosition;
    void Start()
    {
        try
        {
            originalPosition = text.transform.localPosition;
            targetPosition = new Vector3(100,0,0);
        }
        catch(NullReferenceException)
        {
            return;
        } 
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        try
        {
            highlightImage.enabled = true;
            text.transform.localPosition = targetPosition;
        }
        catch(NullReferenceException)
        {
            return;
        }
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        try
        {
            highlightImage.enabled = false;
            text.transform.localPosition = originalPosition;
        }
        catch(NullReferenceException)
        {
            return;
        }
    }
    IEnumerator ButtonAnimation(Vector3 targetPosition)
    {
        yield break;
    }
    public void LoadData()
    {
        XmlSerializer loadData = new XmlSerializer(typeof(DataMembers));
        StreamReader sr = new StreamReader("Abducted Save File");
        DataMembers dm = (DataMembers)loadData.Deserialize(sr);  
        PlayerData.Instance.Stage = dm.level;
        PlayerData.Instance.PlayerHealth = dm.health;
        PlayerData.Instance.PlayerOxygen= dm.oxygen;
        PlayerData.Instance.PlayerPosition = dm.position;
        PlayerData.Instance.PlayerRotation = dm.rotation;
        GunManager.Instance.WeaponEquipped = dm.weaponEquipped;
        // dm.itemList = ItemList.Instance.Itemlist;
        GunManager.Instance.BulletsLoaded = dm.bulletsLoaded;
        GunManager.Instance.TotalBullets = dm.totalBullets;
        SceneManager.LoadScene("MockScene");
    }
}
