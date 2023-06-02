using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private static CanvasManager instance;
    [SerializeField] CanvasGroup inventoryCanvas;
    [SerializeField] Canvas interactCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas mainCanvas;
    [SerializeField] Canvas puzzleOneCanvas;
    [SerializeField] Canvas puzzleTwoCanvas;
    [SerializeField] Canvas puzzleThreeCanvas;
    public static CanvasManager Instance { get { return instance; } }
    public CanvasGroup InventoryCanvas { get => inventoryCanvas; set => inventoryCanvas = value; }
    public Canvas InteractCanvas { get => interactCanvas; set => interactCanvas = value; }
    public Canvas MainCanvas { get => mainCanvas; set => mainCanvas = value; }
    public Canvas PauseCanvas { get => pauseCanvas; set => pauseCanvas = value; }
    public Canvas PuzzleOneCanvas { get => puzzleOneCanvas; set => puzzleOneCanvas = value; }

    [SerializeField] public GameObject[] gunImages;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
