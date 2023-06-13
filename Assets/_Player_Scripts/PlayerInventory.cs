using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;
using System.Linq;
using System;

public class PlayerInventory : MonoBehaviour
{
    [SerializeField] Canvas inventoryCanvas;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] AudioSource generalSound;
    InventoryController inventoryController;
    PlayerControls playerControls;
    InputAction openInventoryButton,interactButton,dropButton;
    bool inventoryOpen = false;
    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as  InventoryController;
    }
    void Start()
    {

    }
    void Update()
    { 
        if(inventoryOpen)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        if(ControlsManager.Instance.IsTabDown)
        {
            OpenInventory();
        }
        if(ControlsManager.Instance.IsDropButtonDown)
        {
            DropItem();
        }
    }
    void OpenInventory()
    {
        AudioManager.Instance.PlaySound(generalSound,"Inventory Open");
        inventoryOpen = !inventoryOpen;
        if(inventoryOpen) 
        {   
            inventoryController.enabled = true;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerAnimation>().enabled = false; 
            gameObject.GetComponent<PlayerShootingScript>().enabled = false; 
            Cursor.lockState = CursorLockMode.None;
            inventoryCanvas.GetComponent<CanvasGroup>().alpha = 1;
            mainCanvas.enabled = false;
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        } 
        else
        {
            inventoryController.enabled = false;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            gameObject.GetComponent<PlayerAnimation>().enabled = true; 
            gameObject.GetComponent<PlayerShootingScript>().enabled = true; 
            Cursor.lockState = CursorLockMode.Locked;
            inventoryCanvas.GetComponent<CanvasGroup>().alpha = 0;
            mainCanvas.enabled = true;
            Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        }
    }
    void DropItem()
    {   
        try
        {
            GunManager.Instance.CheckForWeapon();
            ItemList.Instance.DropItem(inventoryController.selectedItem.itemData.name);  
            inventoryController.DeleteItem(inventoryController.selectedItem);
        }catch(NullReferenceException)
        {
            return;
        }             
    }
}
