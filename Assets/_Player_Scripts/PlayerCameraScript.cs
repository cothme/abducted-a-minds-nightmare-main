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
    public static bool isAiming = false;
    float scroll;
    Vector2 mouseScroll;

    private void OnEnable()
    {
        leftClickControl.action.Enable();
    }
    private void OnDisable()
    {
        leftClickControl.action.Disable();
    }

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if(leftClickControl.action.triggered)
        {
            isAiming = !isAiming;
            StartAim();
        }
        else
        {
            var camera = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = cameraZoomAmount(5);
        }
        if(isAiming)
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
    void StartAim()
    {
        if(isAiming)
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
}
