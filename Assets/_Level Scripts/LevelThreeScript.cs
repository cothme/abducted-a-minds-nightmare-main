using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelThreeScript : MonoBehaviour
{
    [SerializeField] GameObject supplyWall;
    [SerializeField] GameObject firstDoor;
    [SerializeField] GameObject dialogue1;
    [SerializeField] GameObject bossDoor;
    float enemiesDefeated;
    [SerializeField] GameObject[] enemies;
    bool a = false, b = false;
    float gemsCollectedLvlThree = 0;
    private void Update()
    {
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
        
    }
}
