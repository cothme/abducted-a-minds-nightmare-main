using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShootingScript : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] GameObject bulletPreFab;
    [SerializeField] Transform bulletTransform;
    [SerializeField] float bulletMissDistance = 25f;
    void Start()
    {
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {
        if(PlayerCameraScript.isAiming)
        {
            if(Input.GetButtonDown("Fire1"))
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
        // if(bullet != null)
        // {
        //     bullet.transform.position = bulletTransform.transform.position;
        //     bullet.transform.rotation = Quaternion.identity;
        //     bullet.SetActive(true);
        // }
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
