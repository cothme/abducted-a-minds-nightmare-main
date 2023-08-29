using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class LevelThreeScript : MonoBehaviour
{
    [SerializeField] GameObject supplyWall;
    [SerializeField] GameObject firstDoor;
    [SerializeField] GameObject dialogue1;
    [SerializeField] GameObject bossDoor;
    [SerializeField] GameObject[] enemies;
    [SerializeField] PlayableDirector levelThreeDefeatedCutscene;
    bool a = false, b = false;
    float gemsCollectedLvlThree = 0;
    private void Update()
    {
        
        // if(Input.GetKeyDown(KeyCode.L))
        // {
        //     ItemList.Instance.ClearInventoryItems();
        //     SceneManager.LoadScene("Level 4");
        //     // Cursor.lockState = CursorLockMode.None;
        //     // levelThreeDefeatedCutscene.Play();
        // }
    
        if(PlayerState.Instance.IsPuzzleThreeSolved && a == false)
        {
            firstDoor.tag = "Door";
            Destroy(supplyWall);
            dialogue1.GetComponent<DialogueScript>().showText("I can unlock the first door now",2f);
            a = true;
        }
        if(PlayerData.Instance.GemsCollected >= 2 && b == false)
        {
            PlayerState.Instance.LevelThreeCageUnlocked = true;
            bossDoor.tag = "Door";
        }
        if(enemies[0] == null && enemies[1] == null)
        {
            levelThreeDefeatedCutscene.Play();
            Cursor.lockState = CursorLockMode.None;
        }   
    }
}
