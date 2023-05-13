using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;
using System.Linq;

public class PlayerInteractScript : MonoBehaviour
{
    [SerializeField] InputActionReference interactControl;
    [SerializeField] CanvasGroup inventoryCanvas;
    [SerializeField] Canvas interactCanvas;
    [SerializeField] Transform cameraTransform;
    InventoryController inventoryController;
    PlayerControls playerControls;
    InputAction openInventoryButton,interactButton;
    bool inventoryOpen = false;
    RaycastHit hit;
    Ray ray;
    public LayerMask layerMask;
    GameObject lastLookedObject;
    #region Input Setup
    private void Awake()
    {
        inventoryController = FindObjectOfType(typeof(InventoryController)) as  InventoryController;
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        interactButton = playerControls.Player.InteractPickupItem;
        interactButton.Enable();
        openInventoryButton = playerControls.Player.OpenInventory;
        openInventoryButton.Enable();
        openInventoryButton.performed += OpenInventory;
        interactControl.action.Enable();
    }
    private void OnDisable()
    {   
        interactButton.Disable();
        openInventoryButton.Disable();
        interactControl.action.Disable();
    }
    #endregion
    void Start()
    {
        cameraTransform = Camera.main.transform;
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    { 
        LookAtItem();
    }
    void OpenInventory(InputAction.CallbackContext context)
    {
        inventoryOpen = !inventoryOpen;
        if(inventoryOpen) 
        {   
            Cursor.lockState = CursorLockMode.None;
            inventoryCanvas.alpha = 1;
            Cursor.visible = true;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = false;
        } 
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            inventoryCanvas.alpha = 0;
            Cursor.visible = false;
            GameObject.Find("Main Camera").GetComponent<CinemachineBrain>().enabled = true;
        }
    }
    void LookAtItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(cameraTransform.position, cameraTransform.TransformDirection(Vector3.forward), out hit,15, layerMask))
        {
            if(hit.collider.tag == "Item")
            {
                hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                lastLookedObject = hit.collider.gameObject;
                string itemName = lastLookedObject.name;
                interactCanvas.enabled = true;
                PickUpItem(lastLookedObject,itemName);
            }
            else if(lastLookedObject is null)
            {
                return;
            }
            else
            {
                if(lastLookedObject == null)
                {
                    return;
                }
                else
                {
                    lastLookedObject.GetComponent<Renderer>().material.color = Color.white;
                    interactCanvas.enabled = false;
                }
            }
        } 
    }
    void PickUpItem(GameObject itemObject,string itemName)
    {       
        if(playerControls.Player.InteractPickupItem.triggered)
        {
            ItemList.Instance.AddItem(itemName);
            inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
            interactCanvas.enabled = false;
            Destroy(itemObject);
        } 
    }
    void GameplayPause(bool enabled)
    {
        
    }
}
