using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] CanvasGroup inventoryCanvas;
    [SerializeField] Canvas pressEtoInsertCanvas;
    [SerializeField] Canvas pauseCanvas;
    [SerializeField] Canvas mainCanvas;
    private static CanvasManager instance;
    public static CanvasManager Instance { get { return instance; } }
    public CanvasGroup InventoryCanvas { get => inventoryCanvas; set => inventoryCanvas = value; }
    public Canvas PressEtoInsertCanvas { get => pressEtoInsertCanvas; set => pressEtoInsertCanvas = value; }
    public Canvas MainCanvas { get => mainCanvas; set => mainCanvas = value; }
    public Canvas PauseCanvas { get => pauseCanvas; set => pauseCanvas = value; }

    private void Awake()
    {
        // Ensure there is only one instance of InputManager
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
