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
    [SerializeField] Canvas interactCanvas;
    [SerializeField] Canvas puzzleOneCanvas;
    [SerializeField] TextMeshProUGUI itemNameUI;
    [SerializeField] TextMeshProUGUI storyText;
    InventoryController inventoryController;
    PlayerControls playerControls;
    InputAction interactButton;
    int lookDistance = 10;
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
            storyText.text =  gameObject.GetComponent<StoryScript>().Sentence;
            Cursor.lockState = CursorLockMode.None;
        }
    }
    private void CalculateTargetPosition(GameObject gameObject)
    {
        Vector3 leftDirection = transform.right;  // Get the left direction based on object's rotation
        doorTargetPosition = gameObject.transform.position + leftDirection * 30;  // Calculate the target position
    }
    private IEnumerator DoorBehaviour(GameObject gameObject)
    {
        float elapsedTime = 0f;
        Vector3 originalPosition = gameObject.transform.position;

        while (elapsedTime < 3.0f)
        {
            gameObject.transform.position = Vector3.Lerp(originalPosition, doorTargetPosition, elapsedTime / 3.0f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        gameObject.transform.position = doorTargetPosition;

        yield return new WaitForSeconds(2.0f);

        elapsedTime = 0f;

        while (elapsedTime < 3.0f)
        {
            CalculateTargetPosition(gameObject);
            gameObject.transform.position = Vector3.Lerp(doorTargetPosition, originalPosition, elapsedTime / 3.0f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
        gameObject.transform.position = originalPosition;
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
