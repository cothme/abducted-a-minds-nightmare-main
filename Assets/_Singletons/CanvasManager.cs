using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] CanvasGroup inventoryCanvas;
    [SerializeField] Canvas pressEtoInsertCanvas;
    private static CanvasManager instance;
    public static CanvasManager Instance { get { return instance; } }

    public CanvasGroup InventoryCanvas { get => inventoryCanvas; set => inventoryCanvas = value; }
    public Canvas PressEtoInsertCanvas { get => pressEtoInsertCanvas; set => pressEtoInsertCanvas = value; }

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
