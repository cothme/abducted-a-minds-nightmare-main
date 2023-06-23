using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsManager : MonoBehaviour
{
    private static ControlsManager instance;
    public static ControlsManager Instance { get { return instance; } }
    private bool isJumpButtonDown;
    private bool isFireButtonDown;
    private bool isTabDown = false;
    private bool isInteractButtonDown;
    private bool isDropButtonDown;
    private bool isRifleButtonDown;
    private bool isShotgunButtonDown;
    private bool isPistolButtonDown;
    private bool isKnifeButtonDown;
    private bool isReloadButtonDown;
    private bool isPauseButtonDown;
    private bool isUnequipButtonDown;
    private bool isMaskEquipButtonDown;
    private bool isHealthUseButtonDown;
    private bool isFlashlightButtonDown;
    private bool isOxygenKitUseButtonDown;
    private bool isAimButtonDown;
    public bool IsJumpButtonDown { get { return isJumpButtonDown; } }
    public bool IsFireButtonDown { get { return isFireButtonDown; } }
    public bool IsTabDown { get => isTabDown; set => isTabDown = value; }
    public bool IsInteractButtonDown { get => isInteractButtonDown; set => isInteractButtonDown = value; }
    public bool IsDropButtonDown { get => isDropButtonDown; set => isDropButtonDown = value; }
    public bool IsRifleButtonDown { get => isRifleButtonDown; set => isRifleButtonDown = value; }
    public bool IsShotgunButtonDown { get => isShotgunButtonDown; set => isShotgunButtonDown = value; }
    public bool IsPistolButtonDown { get => isPistolButtonDown; set => isPistolButtonDown = value; }
    public bool IsKnifeButtonDown { get => isKnifeButtonDown; set => isKnifeButtonDown = value; }
    public bool IsReloadButtonDown { get => isReloadButtonDown; set => isReloadButtonDown = value; }
    public bool IsPauseButtonDown { get => isPauseButtonDown; set => isPauseButtonDown = value; }
    public bool IsUnequipButtonDown { get => isUnequipButtonDown; set => isUnequipButtonDown = value; }
    public bool IsMaskEquipButtonDown { get => isMaskEquipButtonDown; set => isMaskEquipButtonDown = value; }
    public bool IsHealthUseButtonDown { get => isHealthUseButtonDown; set => isHealthUseButtonDown = value; }
    public bool IsFlashlightButtonDown { get => isFlashlightButtonDown; set => isFlashlightButtonDown = value; }
    public bool IsOxygenKitUseButtonDown { get => isOxygenKitUseButtonDown; set => isOxygenKitUseButtonDown = value; }
    public bool IsAimButtonDown { get => isAimButtonDown; set => isAimButtonDown = value; }

    private void Update()
    {
        isJumpButtonDown = Input.GetButtonDown("Jump");
        isFireButtonDown = Input.GetButtonDown("Fire1");
        isTabDown = Input.GetKeyDown(KeyCode.Tab);
        isInteractButtonDown = Input.GetKeyDown(KeyCode.E);
        isDropButtonDown = Input.GetKeyDown(KeyCode.X);
        isRifleButtonDown = Input.GetKeyDown(KeyCode.Alpha1);
        isShotgunButtonDown = Input.GetKeyDown(KeyCode.Alpha2);
        isPistolButtonDown = Input.GetKeyDown(KeyCode.Alpha3);
        isKnifeButtonDown = Input.GetKeyDown(KeyCode.Alpha4);
        isReloadButtonDown = Input.GetKeyDown(KeyCode.R);
        isPauseButtonDown = Input.GetKeyDown(KeyCode.Escape);
        isUnequipButtonDown = Input.GetKeyDown(KeyCode.Alpha5);
        isMaskEquipButtonDown = Input.GetKeyDown(KeyCode.M);
        isHealthUseButtonDown = Input.GetKeyDown(KeyCode.H);
        isFlashlightButtonDown = Input.GetKeyDown(KeyCode.F);
        isOxygenKitUseButtonDown = Input.GetKeyDown(KeyCode.O);
        isAimButtonDown = Input.GetMouseButtonDown(1);
    }
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
