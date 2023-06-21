using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;
using TMPro;
using Cinemachine;
using System.Linq;
using System;
using System.Xml.Serialization;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInteractScript : MonoBehaviour
{
    [SerializeField] GameObject normalCamera;
    [SerializeField] GameObject aimCamera;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] GameObject bag;
    [SerializeField] PlayableDirector doorUnlockedPlayableDirector;
    [SerializeField] PlayableDirector bossAppear;
    [SerializeField] Canvas interactCanvas;
    [SerializeField] Image storyImage;
    [SerializeField] Canvas puzzleOneCanvas;
    [SerializeField] Canvas puzzleTwoCanvas;
    [SerializeField] Canvas storyCanvas;
    [SerializeField] TextMeshProUGUI itemNameUI;
    [SerializeField] TextMeshProUGUI storyText;
    [SerializeField] AudioSource generalSound;
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
        if(puzzleOneCanvas.enabled == true || puzzleTwoCanvas.enabled == true)
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
            else if(hit.collider.tag == "Folder")
            {
                itemNameUI.text = "Press E to Open Folder";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject, "Folder");
            }
            else if (hit.collider.tag == "JournalPage")
            {
                itemNameUI.text = "Press E to read journal page";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject, "JournalPage");
            }
            else if (hit.collider.tag == "UVPaperFile")
            {
                itemNameUI.text = "Press E to read";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject, "UVPaperFile");
            }
            else if (hit.collider.tag == "Bag")
            {
                itemNameUI.text = "Press E to pick up Bag";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject, "Bag");
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
            else if(hit.collider.tag == "SavePoint")
            {
                itemNameUI.text = "Press E to save";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject,"SavePoint");
            }
            else if(hit.collider.tag == "BossInitiate")
            {
                itemNameUI.text = "Press E";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject,"BossInitiate");
            }
            else if(hit.collider.tag == "Button")
            {
                itemNameUI.text = "Press E to press button";
                interactCanvas.enabled = true;
                Interact(hit.collider.gameObject,"Button");
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
            AudioManager.Instance.PlaySound(generalSound,"Pick Up");
            gameObject.GetComponent<Animator>().Play("Pick Up Item");
            ItemList.Instance.AddItem(itemName);
            inventoryController.InsertRandomItem(ItemList.Instance.Itemlist.Last());
            interactCanvas.enabled = false;
            itemObject.SetActive(false);
        }
    }
    void Interact(GameObject gameObject, string colliderTag)
    {       
        if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Puzzle")
        {
            DisableScripts(true);
            if(SceneManager.GetActiveScene().name == "level 1")
            {
                puzzleOneCanvas.enabled = true;
            }
            else if(SceneManager.GetActiveScene().name == "level 2")
            {
                puzzleTwoCanvas.enabled = true;
            }
        } 
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Door")
        {   
            AudioManager.Instance.PlaySound(gameObject.GetComponent<AudioSource>(),"Door Open");
            this.gameObject.GetComponent<Animator>().Play("Open Door");
            gameObject.GetComponent<Animator>().Play("Door");
        }
        else if (ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Folder")
        {
            AudioManager.Instance.PlaySound(generalSound, "Open Story");
            gameObject.GetComponent<Animator>().Play("Folder Open");
            gameObject.GetComponent<Collider>().enabled = false;
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "UVPaperFile")
        {            
            AudioManager.Instance.PlaySound(generalSound,"Open Story");            
            storyImage.sprite = gameObject.GetComponent<StoryScript>().UVsprite;
            itemNameUI.text = "";
            DisableScripts(true);
            storyCanvas.enabled = true;
            storyText.text = gameObject.GetComponent<StoryScript>().Sentence;
            Cursor.lockState = CursorLockMode.None;
            if(gameObject.GetComponent<DialogueScript>() == null)
            {
                return;
            }
            else
            {
                gameObject.GetComponent<DialogueScript>().showText(gameObject.GetComponent<DialogueScript>().subtitle,gameObject.GetComponent<DialogueScript>().deletionTime);
            }
        }
        else if (ControlsManager.Instance.IsInteractButtonDown && colliderTag == "JournalPage")
        {
            AudioManager.Instance.PlaySound(generalSound, "Open Story");
            DisableScripts(true);
            storyCanvas.enabled = true;
            storyImage.sprite = gameObject.GetComponent<StoryScript>().UVsprite;
            itemNameUI.text = "";
            storyText.text = gameObject.GetComponent<StoryScript>().Sentence;
            Cursor.lockState = CursorLockMode.None;
            if(gameObject.GetComponent<DialogueScript>() == null)
            {
                return;
            }
            else
            {
                gameObject.GetComponent<DialogueScript>().showText(gameObject.GetComponent<DialogueScript>().subtitle,gameObject.GetComponent<DialogueScript>().deletionTime);
            }
        }
        else if (ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Bag")
        {
            AudioManager.Instance.PlaySound(generalSound, "Pick Up");
            bag.SetActive(true);
            gameObject.SetActive(false);
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Reader")
        {  
            if(SceneManager.GetActiveScene().name == "level 1")
            {
                PlayerState.Instance.LevelOneDoorUnlocked = true;
                doorUnlockedPlayableDirector.Play();
                foreach(InventoryItem i in ItemList.Instance.InventoryItems)
                {
                        if(i.itemData.name == "KeyCard")
                        {
                        inventoryController.selectedItem = i;
                        inventoryController.DeleteItem(inventoryController.selectedItem);
                        break;
                        }
                }
                ItemList.Instance.DropItem("Keycard");
            }
            else if(SceneManager.GetActiveScene().name == "level 2")
            {
                PlayerState.Instance.LevelTwoDoorUnlocked = true;
                doorUnlockedPlayableDirector.Play();
                foreach(InventoryItem i in ItemList.Instance.InventoryItems)
                {
                        if(i.itemData.name == "KeyCard")
                        {
                        inventoryController.selectedItem = i;
                        inventoryController.DeleteItem(inventoryController.selectedItem);
                        break;
                        }
                }
                ItemList.Instance.DropItem("Keycard");
            }
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "SavePoint")
        {
            PlayerData.Instance.IsSessionSaved = true;
            PlayerData.Instance.PlayerPosition = this.gameObject.transform.position;
            PlayerData.Instance.PlayerRotation = this.gameObject.transform.rotation;
            DataMembers dm = new DataMembers();
            dm.level = PlayerData.Instance.Stage;
            dm.isSessionSaved = PlayerData.Instance.IsSessionSaved;
            dm.health = PlayerData.Instance.PlayerHealth;
            dm.oxygen = PlayerData.Instance.PlayerOxygen;
            dm.position = PlayerData.Instance.PlayerPosition;
            dm.rotation = PlayerData.Instance.PlayerRotation;
            dm.weaponEquipped = GunManager.Instance.WeaponEquipped;
            dm.itemList = ItemList.Instance.Itemlist;
            dm.bulletsLoaded = GunManager.Instance.BulletsLoaded;
            dm.totalBullets = GunManager.Instance.TotalBullets;
            dm.canEquipRifle = GunManager.Instance.CanEquipRifle;
            dm.canEquipShotgun = GunManager.Instance.CanEquipShotgun;
            dm.canEquipPistol = GunManager.Instance.CanEquipPistol;
            dm.canEquipKnife = GunManager.Instance.CanEquipKnife;
            dm.aiming = PlayerState.Instance.Aiming;
            dm.reloading = PlayerState.Instance.Reloading;
            dm.running = PlayerState.Instance.Running;
            dm.isPuzzleOneSolved = PlayerState.Instance.IsPuzzleOneSolved;
            dm.levelOneDoorUnlocked = PlayerState.Instance.LevelOneDoorUnlocked;
            dm.levelOneCageUnlocked = PlayerState.Instance.LevelOneCageUnlocked;
            XmlSerializer saveData = new XmlSerializer(typeof(DataMembers));
            StreamWriter sw = new StreamWriter("Abducted Save File");
            saveData.Serialize(sw,dm);
            sw.Close();
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "BossInitiate")
        {
            if(PlayerData.Instance.Stage == 1)
            {
                bossAppear.Play();
                PlayerState.Instance.LevelOneCageUnlocked = true;
            }
            if(PlayerData.Instance.Stage == 2)
            {
                // bossAppear.Play();
                PlayerState.Instance.LevelTwoCageUnlocked = true;
            }
            
        }
        else if(ControlsManager.Instance.IsInteractButtonDown && colliderTag == "Button")
        {
            if(SceneManager.GetActiveScene().name == "level 2")
            {
                gameObject.GetComponent<LevelTwoScript>().doorPuzzle.SetActive(false);
                gameObject.GetComponent<DialogueScript>().showText(gameObject.GetComponent<DialogueScript>().subtitle,gameObject.GetComponent<DialogueScript>().deletionTime);
            }
        }
    }
    public void ExitStoryText()
    {
        DisableScripts(false);
    }
    public void ExitPuzzle2()
    {
        DisableScripts(false);
    }
    public void ExitPuzzle()
    {
        if(PlayerState.Instance.IsPuzzleOneSolved)
        {
            gameObject.GetComponent<DialogueScript>().showText("Uhhh what? The answer for that puzzle is… my birthday? Okay?",5);
        }
        else
        {
            gameObject.GetComponent<DialogueScript>().showText("I didn't expect that it will be THIS hard! Let's see if there's any clues here. ",5);
        }
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
