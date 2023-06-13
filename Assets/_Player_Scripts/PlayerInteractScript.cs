using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using TMPro;
using Cinemachine;
using System.Linq;
using System;

public class PlayerInteractScript : MonoBehaviour
{
    [SerializeField] PlayableDirector doorUnlockedPlayableDirector;
    [SerializeField] Canvas interactCanvas;
    [SerializeField] Canvas puzzleOneCanvas;
    [SerializeField] Canvas storyCanvas;
    [SerializeField] TextMeshProUGUI itemNameUI;
    [SerializeField] TextMeshProUGUI storyText;
    InventoryController inventoryController;
    PlayerControls playerControls;
    InputAction interactButton;
    int lookDistance = 15;
    Color currentColor;
    RaycastHit hit;
    Ray ray;
    public LayerMask layerMask;
    GameObject lastLookedObject;
    Vector3 doorTargetPosition;
    void Start()
    {
        inventoryController = GameObject.Find("Main Camera").GetComponent<InventoryController>();
        Cursor.lockState = CursorLockMode.Locked;

    }
    void Update()
    { 
        if(puzzleOneCanvas.enabled == true)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        LookAtItem();
    }
    void LookAtItem()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.TransformDirection(Vector3.forward), out hit,lookDistance, layerMask))
        {
            if(hit.collider.tag == "Item")
            {
                lastLookedObject = hit.collider.gameObject;
                string itemName = lastLookedObject.name;
                itemNameUI.text = "Press E to pick up " + itemName;
                interactCanvas.enabled = true;
                Interact(lastLookedObject,itemName,"Item");
            }
            else if(hit.collider.tag == "Puzzle")
            {
                itemNameUI.text = "Press E to play puzzle";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject,"Puzzle");   
            }
            else if(hit.collider.tag == "Door")
            {
                itemNameUI.text = "Press E to Open";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject,"Door");
            }
            else if(hit.collider.tag == "StoryItem")
            {
                itemNameUI.text = "Press E to read";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject,"StoryItem");
            }
            else if(hit.collider.tag == "Reader")
            {
                if(ItemList.Instance.Itemlist.Contains(8))
                {
                    itemNameUI.text = "Press E to unlock door using Keycard";
                    interactCanvas.enabled = true;
                    Interact(hit.collider.gameObject,"Reader");
                }
                else
                {
                    itemNameUI.text = "You need a keycard to open this door!";
                    interactCanvas.enabled = true;
                }
            }
            else if(lastLookedObject is null)
            {
                return;
            }
            else
            {
                interactCanvas.enabled = false;
                if(lastLookedObject == null)
                {
                    return;
                }
                else
                {
                    interactCanvas.enabled = false;
                }
            }
        }
        else
        {
            interactCanvas.enabled = false;
        } 
    }
    void Interact(GameObject itemObject,string itemName,string colliderTag)
    {       
        if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Item")
        {
            gameObject.GetComponent<Animator>().Play("Pick Up Item");
            ItemList.Instance.AddItem(itemName);
            inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
            interactCanvas.enabled = false;
            Destroy(itemObject);
        }
    }
    void Interact(GameObject gameObject, string colliderTag)
    {       
        if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Puzzle")
        {
            DisableScripts(true);
            puzzleOneCanvas.enabled = true;
        } 
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Door")
        {   
            gameObject.GetComponent<Animator>().Play("Door");
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "StoryItem")
        {
            DisableScripts(true);
            storyCanvas.enabled = true;
            interactCanvas.enabled = false;
            storyText.text =  gameObject.GetComponent<StoryScript>().Sentence;
            Cursor.lockState = CursorLockMode.None;
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Reader")
        {  
            PlayerState.Instance.LevelOneDoorUnlocked = true;
            doorUnlockedPlayableDirector.Play();
            ItemList.Instance.DropItem("Keycard");
        }
    }
    public void ExitStoryText()
    {
        DisableScripts(false);

    }
    public void ExitPuzzle()
    {
        DisableScripts(false);
    }
    public void DisableScripts(bool isOn)
    {
        if(isOn)
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponent<PlayerMovement>().enabled = false;
            gameObject.GetComponent<PlayerShootingScript>().enabled = false;
            gameObject.GetComponent<PlayerAnimation>().enabled = false;
            gameObject.GetComponent<PlayerInventory>().enabled = false;
            Camera.main.GetComponent<CinemachineBrain>().enabled = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.GetComponent<PlayerMovement>().enabled = true;
            gameObject.GetComponent<PlayerShootingScript>().enabled = true;
            gameObject.GetComponent<PlayerAnimation>().enabled = true;
            gameObject.GetComponent<PlayerInventory>().enabled = true;
            Camera.main.GetComponent<CinemachineBrain>().enabled = true;
        }
    }
}
