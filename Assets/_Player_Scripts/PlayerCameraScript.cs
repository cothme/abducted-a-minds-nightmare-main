using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class PlayerCameraScript : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] InputActionReference leftClickControl;
    [SerializeField] Transform playerTransform;
    [SerializeField] Canvas aimCanvas;
    [SerializeField] AudioSource generalSound;
    CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed;
    public int priorityCameraNumber;
    Transform cameraTransform;
    bool isCameraCentered;
    float scroll;
    Vector2 mouseScroll;
    PlayerControls playerControls;
    InputAction aimButton;
    InputAction middleMouseButton;
    bool exitAim;
    void Awake()
    { 
        playerControls = new PlayerControls();
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = Camera.main.transform;
    }
    private void OnEnable()
    {
        aimButton = playerControls.Player.Aim;
        aimButton.Enable();
        middleMouseButton = playerControls.Player.CenterCamera;
        middleMouseButton.Enable();
        aimButton.performed += StartAim;
        middleMouseButton.performed += MiddleMouseClicked;
        middleMouseButton.canceled += MiddleMouseReleased;
        leftClickControl.action.Enable();
    }
    private void OnDisable()
    {
        middleMouseButton.Disable();
        aimButton.Disable();
        leftClickControl.action.Disable();
    }
    void Update()
    {
        RecenterCamera();
        PlayerRotateBaseOnCamera(playerTransform,cameraTransform);
    }
    private void PlayerRotateBaseOnCamera(Transform playerTransform, Transform cameraTransform)
    {
        if(PlayerState.Instance.Aiming)
        {
            Quaternion targetRotation = Quaternion.Euler(cameraTransform.eulerAngles.x, cameraTransform.eulerAngles.y, 0);
            playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            exitAim = true;
        }
        else
        {
            if(exitAim)
            {
                Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
                playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, 1);
            }
            exitAim = false;
        }
    }
    void StartAim(InputAction.CallbackContext context)
    {
        if(GunManager.Instance.WeaponEquipped == null || PlayerState.Instance.Reloading)
        {
            return;
        }
        else
        {
            PlayerState.Instance.Aiming = !PlayerState.Instance.Aiming;
            if(PlayerState.Instance.Aiming && GunManager.Instance.WeaponEquipped == "Pistol")
            {
                animator.Play("HG Aim");
                AudioManager.Instance.PlaySound(generalSound,"Aim");
                aimCanvas.enabled = true;
                virtualCamera.Priority = 9;
            }
            else if(PlayerState.Instance.Aiming && GunManager.Instance.WeaponEquipped == "Knife")
            {
                animator.Play("KF Aim"); 
                aimCanvas.enabled = true;
                virtualCamera.Priority = 9;
            }
            else if(PlayerState.Instance.Aiming && GunManager.Instance.WeaponEquipped == "Rifle")
            {
                animator.Play("Rif Aim"); 
                aimCanvas.enabled = true;
                virtualCamera.Priority = 9;
            }
            else if(PlayerState.Instance.Aiming && GunManager.Instance.WeaponEquipped == "Shotgun")
            {
                animator.Play("SG Aim"); 
                aimCanvas.enabled = true;
                virtualCamera.Priority = 9;
            }
            else
            {
                aimCanvas.enabled = false;
                virtualCamera.Priority = 10;
            }  
        }
    }
    void RecenterCamera()
    {
        var vertical = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        var horizontal = virtualCamera.GetCinemachineComponent<CinemachinePOV>();
        if(isCameraCentered)
        {
            vertical.m_HorizontalRecentering.m_enabled = true;
            horizontal.m_VerticalRecentering.m_enabled = true;
        }
        else
        {
            vertical.m_HorizontalRecentering.m_enabled = false;
            horizontal.m_VerticalRecentering.m_enabled = false; 
        }
    }
    void MiddleMouseClicked(InputAction.CallbackContext context)
    {
        isCameraCentered = true;
    }
    void MiddleMouseReleased(InputAction.CallbackContext context)
    {
        isCameraCentered = false;
    }
}
