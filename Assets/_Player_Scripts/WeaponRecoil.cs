using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class WeaponRecoil : MonoBehaviour
{
    CinemachineVirtualCamera aimCamera;
    public float recoilAmount = 2f;
    public float recoilResetSpeed = 1f;
    private float originalYRotation;
    private float targetYRotation;
    private bool isShooting;
    float currentRecoilPosition, finalRecoilPosition;

    void Start()
    {
        aimCamera = GetComponent<CinemachineVirtualCamera>(); 
        originalYRotation = aimCamera.GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_VerticalAxis.Value;
        targetYRotation = originalYRotation;
    }
    void Update()
    {
        if (isShooting)
        {
            targetYRotation -= 1f;
            if(targetYRotation <= -5f)
            {
                targetYRotation = -5f;
            }
            targetYRotation = Mathf.Lerp(targetYRotation, originalYRotation, Time.deltaTime * recoilResetSpeed);
        }
        else
        {
            targetYRotation = Mathf.Lerp(targetYRotation, originalYRotation, Time.deltaTime * recoilResetSpeed);
            aimCamera.GetCinemachineComponent<Cinemachine.CinemachinePOV>().m_VerticalAxis.Value = targetYRotation;
        }
    }
    public void StartShooting()
    {
        StartCoroutine(RecoilCoroutine());
    }
    IEnumerator RecoilCoroutine()
    {
        isShooting = true;
        yield return new WaitForSeconds(0.1f);
        isShooting = false;
    }
}
