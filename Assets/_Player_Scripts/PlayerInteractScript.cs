using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Cinemachine;
using System.Linq;

public class PlayerInteractScript : MonoBehaviour
{
    [SerializeField] InputActionReference tabKeyControl;
    [SerializeField] InputActionReference interactControl;
    [SerializeField] CanvasGroup inventoryCanvas;
    [SerializeField] Canvas interactCanvas;
    [SerializeField] Transform cameraTransform;
    InventoryController inventoryController;
    bool inventoryOpen = false;
    RaycastHit hit;
    Ray ray;
    public LayerMask layerMask;
    GameObject lastLookedObject;
    // Start is called before the first frame update
    private void OnEnable()
    {
        tabKeyControl.action.Enable();
        interactControl.action.Enable();
    }
    private void OnDisable()
    {
        tabKeyControl.action.Disable();
        interactControl.action.Disable();
    }
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

    // Update is called once per frame
    void Update()
    {
        if(tabKeyControl.action.triggered)
        {
            OpenInventory();
        }
        LookAtItem();
    }
    void OpenInventory()
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
        if(interactControl.action.triggered)
        {
            ItemList.Instance.AddItem(itemName);
            inventoryController.InsertRandomItem(ItemList.Instance.itemList.Last());
            // Destroy(itemObject);
        }
    }
    void GameplayPause(bool enabled)
    {
        
    }
}
