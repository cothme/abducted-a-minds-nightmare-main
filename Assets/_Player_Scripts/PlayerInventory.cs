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
        inventoryOpen = !inventoryOpen;
        if(inventoryOpen) 
        {   
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerAnimation>().enabled = false; 
            gameObject.GetComponent<PlayerShootingScript>().enabled = false; 
            Cursor.lockState = CursorLockMode.None;
            CanvasManager.Instance.InventoryCanvas.alpha = 1;
            CanvasManager.Instance.MainCanvas.enabled = false;
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        } 
        else
        {
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            gameObject.GetComponent<PlayerAnimation>().enabled = true; 
            gameObject.GetComponent<PlayerShootingScript>().enabled = true; 
            Cursor.lockState = CursorLockMode.Locked;
            CanvasManager.Instance.InventoryCanvas.alpha = 0;
            CanvasManager.Instance.MainCanvas.enabled = true;
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
