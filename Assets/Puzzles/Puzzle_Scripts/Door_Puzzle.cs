using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Door_Puzzle : MonoBehaviour
{
    public GameObject gameWonPanel;
    [SerializeField] GameObject keyCard;
    [SerializeField] GameObject puzzleObject;
    [SerializeField] List<Transform> doors;
    [SerializeField] List<Button> buttons;
    public int[] correctValues = new int[] {0,1,1,0};
    int[] playerValues = new int[] {0,0,0,0};
    bool[] doorFlipped = new bool[] {false,false,false,false};
    
    IEnumerator RotateDoorCoroutine(int doorIndex)
    {
        if(!doorFlipped[doorIndex])
        {
            doorFlipped[doorIndex] = true;
        }
        else
        {
            doorFlipped[doorIndex] = false;
        }
        Quaternion startRotate = doors[doorIndex].transform.rotation;
        Quaternion endRotate = doors[doorIndex].transform.rotation * Quaternion.Euler(0,180,0);
        float i = 0;
        while (i < 1)
        {
            i += Time.deltaTime * 1;
            doors[doorIndex].transform.rotation = Quaternion.Lerp(startRotate,endRotate,i);
            yield return null;
        }
        determinePlayerValue(doorFlipped,playerValues);
        enableButtons(buttons);
        if(CheckWinCondition())
        {
            gameWonPanel.SetActive(true);
            keyCard.SetActive(true);
            PlayerState.Instance.IsPuzzleTwoSolved = true;
            Destroy(puzzleObject);
        }
    }
    void disableButtons(List<Button> buttons)
    {
        foreach(Button button in buttons)
        {
            button.interactable = false;
        }
    }
    void enableButtons(List<Button> buttons)
    {
        foreach(Button button in buttons)
        {
            button.interactable = true;
        }
    }
    void StartRotate(int doorIndex)
    {
        StartCoroutine(RotateDoorCoroutine(doorIndex));
    }
    void determinePlayerValue(bool[] doorFlipped,int[] playerValues)
    {
        for(int i = 0; i < doorFlipped.Length; i++)
        {
            if(doorFlipped[i]) { playerValues[i] = 1; } else { playerValues[i] = 0; }
        }
    }

    bool CheckWinCondition()
    {
        return Enumerable.SequenceEqual(correctValues,playerValues);
    }
    public void Button1Pressed()
    {   
        disableButtons(buttons);
        StartRotate(0); 
        StartRotate(1);
    }
    public void Button2Pressed()
    {
        disableButtons(buttons); 
        StartRotate(0); 
        StartRotate(1);
        StartRotate(2);
    }
    public void Button3Pressed()
    {
        disableButtons(buttons); 
        StartRotate(1); 
        StartRotate(2);
        StartRotate(3);
    }
    public void Button4Pressed()
    {
        disableButtons(buttons); 
        StartRotate(2); 
        StartRotate(3);
    }
}

