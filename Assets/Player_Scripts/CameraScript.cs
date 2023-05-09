using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraScript : MonoBehaviour
{
    CinemachineVirtualCamera virtualCamera;
    public float rotationSpeed;
    public int priorityCameraNumber;
    [SerializeField] Transform playerTransform;
    Transform cameraTransform;
    public static bool isAiming = false;
    float scroll;

    void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if(Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
            StartAim();
        }
        else
        {
            var camera = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>().m_CameraDistance = MouseScroll(10);
        }
        if(isAiming)
        {
            PlayerRotateBaseOnCamera(playerTransform,cameraTransform);
        }  
    }
    float MouseScroll(int maxValue)
    {
        scroll += Input.mouseScrollDelta.y;
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
            virtualCamera.Priority = 9;
        }
        else
        {
            virtualCamera.Priority = 10;
        }  
    }
}
