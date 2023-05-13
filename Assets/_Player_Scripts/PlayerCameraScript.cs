using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System;

public class PlayerCameraScript : MonoBehaviour
{
    [SerializeField] InputActionReference leftClickControl;
    [SerializeField] Transform playerTransform;
    [SerializeField] Canvas aimCanvas;
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
        var camera = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraZoomAmount(5);
        if(PlayerState.Instance.Aiming)
        {
            PlayerRotateBaseOnCamera(playerTransform,cameraTransform);
        } 
    }
    private void Shoot()
    {
        throw new NotImplementedException();
    }

    float cameraZoomAmount(int maxValue)
    {
        scroll += Mouse.current.scroll.ReadValue().y;
        if(scroll > maxValue)
        {
            scroll = maxValue;
        }     
        else if(scroll <= 3)
        {
            scroll = 3;
        }
        return scroll;        
    }
    private void PlayerRotateBaseOnCamera(Transform playerTransform, Transform cameraTransform)
    {
        Quaternion targetRotation = Quaternion.Euler(0, cameraTransform.eulerAngles.y, 0);
        playerTransform.rotation = Quaternion.Slerp(playerTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
    void StartAim(InputAction.CallbackContext context)
    {
        PlayerState.Instance.Aiming = !PlayerState.Instance.Aiming;
        if(PlayerState.Instance.Aiming)
        {
            aimCanvas.enabled = true;
            virtualCamera.Priority = 9;
        }
        else
        {
            aimCanvas.enabled = false;
            virtualCamera.Priority = 10;
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
