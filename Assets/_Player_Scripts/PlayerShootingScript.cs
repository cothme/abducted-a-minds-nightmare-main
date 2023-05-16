using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootingScript : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] GameObject bulletPreFab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] float bulletMissDistance = 25f;
    PlayerControls playerControls;
    InputAction fireButton;
    #region Input Setup
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
     private void OnEnable()
    {
        fireButton = playerControls.Player.FireSingleBullet;
        fireButton.Enable();
        fireButton.performed += Shoot;
    }
    private void OnDisable()
    {
        fireButton.Disable();
    }
    #endregion
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    private void Shoot(InputAction.CallbackContext context)
    {
        if(PlayerState.Instance.Aiming)
        {
            RaycastHit hit;
            GameObject bullet = Instantiate(bulletPreFab,bulletTransform.position,bulletTransform.rotation);
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
}
