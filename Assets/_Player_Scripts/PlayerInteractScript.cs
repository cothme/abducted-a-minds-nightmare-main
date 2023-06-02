using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;
using System.Linq;
using System;

public class PlayerInteractScript : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemNameUI;
    [SerializeField] Transform cameraTransform;
    InventoryController inventoryController;
    PlayerControls playerControls;
    InputAction openInventoryButton,interactButton,dropButton;
    bool inventoryOpen = false;
    int lookDistance = 10;
    Color currentColor;
    RaycastHit hit;
    Ray ray;
    public LayerMask layerMask;
    GameObject lastLookedObject;
    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as  InventoryController;
    }
    void Start()
    {
        cameraTransform = Camera.main.transform;
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    { 
        LookAtItem();
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
            Cursor.lockState = CursorLockMode.None;
            CanvasManager.Instance.InventoryCanvas.alpha = 1;
            CanvasManager.Instance.MainCanvas.enabled = false;
            Cursor.visible = true;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = false;
        } 
        else
        {
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            Cursor.lockState = CursorLockMode.Locked;
            CanvasManager.Instance.InventoryCanvas.alpha = 0;
            CanvasManager.Instance.MainCanvas.enabled = true;
            Cursor.visible = false;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
        }
    }
    void LookAtItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit,lookDistance, layerMask))
        {
            if(hit.collider.tag == "Item")
            {
                hit.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;
                lastLookedObject = hit.collider.gameObject;
                string itemName = lastLookedObject.name;
                itemNameUI.text = "Press E to pick up " + itemName;
                CanvasManager.Instance.InteractCanvas.enabled = true;
                Interact(lastLookedObject,itemName,"Item");
            }
            else if(hit.collider.tag == "Puzzle")
            {
                itemNameUI.text = "Press E to play puzzle";
                CanvasManager.Instance.InteractCanvas.enabled = true;
                Interact();   
            }
            else if(lastLookedObject is null)
            {
                return;
            }
            else
            {
                CanvasManager.Instance.InteractCanvas.enabled = false;
                if(lastLookedObject == null)
                {
                    return;
                }
                else
                {
                    lastLookedObject.GetComponent<MeshRenderer>().material.color = Color.white;
                    CanvasManager.Instance.InteractCanvas.enabled = false;
                }
            }
        }
        else
        {
            CanvasManager.Instance.InteractCanvas.enabled = false;
        } 
    }
    void Interact(GameObject itemObject,string itemName,string colliderTag)
    {       
        if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Item")
        {
            ItemList.Instance.AddItem(itemName);
            inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
            CanvasManager.Instance.InteractCanvas.enabled = false;
            Destroy(itemObject);
        }
    }
    void Interact()
    {       
        if(ControlsManager.Instance.IsInteractButtonDown)
        {
            CanvasManager.Instance.PuzzleOneCanvas.enabled = true;
            Cursor.lockState = CursorLockMode.None;
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
