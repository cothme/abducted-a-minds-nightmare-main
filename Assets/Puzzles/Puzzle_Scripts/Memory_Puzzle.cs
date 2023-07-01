using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Memory_Puzzle : MonoBehaviour
{
    int maxSize = 5;
    public Sprite imageSprite;
    public Button startButton;
    public GameObject gameWonPanel;
    public GameObject rifle;
    public GameObject puzzleObject;
    [SerializeField] List<Button> buttons;
    [SerializeField] List<Sprite> images = new List<Sprite>{};
    [SerializeField] List<Image> imagesPlaceHolder = new List<Image>{};
    List<int> correctValues = new List<int>{};
    List<int> playerValues = new List<int>{};

    void Start()
    {
        disableButtons(buttons);
    }
    IEnumerator AddRandomValues()
    {
        while(correctValues.Count < maxSize)
        {
            int randomValue = Random.Range(1,4);
            correctValues.Add(randomValue);
            yield return new WaitForSeconds(1f);
        }
    }
    IEnumerator DisplayValues()
    {
        int index = 0;
        int numValuesDisplayed = 0;
        while(numValuesDisplayed < correctValues.Count)
        {
            imagesPlaceHolder[index].sprite = GetImage(correctValues[index]);

            yield return new WaitForSeconds(1f);
            index = (index + 1) % correctValues.Count;
            numValuesDisplayed++;
        }
    }
    IEnumerator AfterDisplayValues()
    {
        yield return new WaitForSeconds(5f);
        foreach(Image placeholder in imagesPlaceHolder)
        {
            placeholder.sprite = imageSprite;
        }
        enableButtons(buttons);
    }
    Sprite GetImage(int value)
    {
        return images[value];
    }    

    public void RightPressed()
    {
        Retry();
        playerValues.Add(2);
    }
    public void LeftPressed()
    {
        Retry();
        playerValues.Add(1);
    }
    public void UpPressed()
    {
        Retry();
        playerValues.Add(3);
    }
    public void DownPressed()
    {
        Retry();
        playerValues.Add(0);
    }
    public void StartClicked()
    {
        startButton.interactable = false;
        disableButtons(buttons);
        StartCoroutine(AfterDisplayValues());
        StartCoroutine(AddRandomValues());
        StartCoroutine(DisplayValues());
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.S))
        {
            foreach(int i in correctValues)
            {
                print("Correct Value " + i);
            }
        }
        if(Input.GetKeyDown(KeyCode.A))
        {
            foreach(int i in playerValues)
            {
                print("Player Value " + i);
            }
        }
        Retry();
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
    void Retry()
    {
        if(playerValues.Count >= 5)
        {
            disableButtons(buttons);
            if(CheckWinCondition())
            {
                gameWonPanel.SetActive(true);
                rifle.SetActive(true);
                playerValues.Clear();
                PlayerState.Instance.IsPuzzleThreeSolved = true;
                Destroy(puzzleObject);
            }
            else
            {
                playerValues.Clear();
                startButton.interactable = true;
                disableButtons(buttons);
            }
        }
    }
    bool CheckWinCondition()
    {
        return Enumerable.SequenceEqual(correctValues,playerValues);
    }
    public void ExitClicked()
    {
        playerValues.Clear();
        startButton.interactable = true;
        disableButtons(buttons);
    }
}
