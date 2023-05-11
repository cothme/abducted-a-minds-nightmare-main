using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] InputActionReference rightClickControl;
    [SerializeField] GameObject bulletPreFab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] float bulletMissDistance = 25f;

     private void OnEnable()
    {
        rightClickControl.action.Enable();
    }
    private void OnDisable()
    {
        rightClickControl.action.Disable();
    }

    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if(PlayerCameraScript.isAiming)
        {
            if(rightClickControl.action.triggered)
            {
                Shoot();
            }
        }
    }
    private void Shoot()
    {
        RaycastHit hit;
        GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,Quaternion.identity);
        BulletScript bulletInstance = bullet.GetComponent<BulletScript>();
        if(Physics.Raycast(cameraTransform.position,cameraTransform.forward,out hit, Mathf.Infinity))
        {
            bulletInstance.target = hit.point;
            bulletInstance.hit = true; 
        }
        else
        {
            bulletInstance.target = cameraTransform.position + cameraTransform.forward * bulletMissDistance;
            bulletInstance.hit = true; 
        }
    }
}
