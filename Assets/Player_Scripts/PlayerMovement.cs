using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed;
    public float rotationSpeed;
    Transform cameraTransform;
    Vector3 input;
    Vector3 movementVector;
    Rigidbody playerRigidBody;
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
        cameraTransform = Camera.main.transform;
    }
    void Update()
    {    
        input = new Vector3(Input.GetAxis("Horizontal"),0, Input.GetAxis("Vertical"));
    }
    void FixedUpdate()
    {
        if(movementVector != Vector3.zero && !CameraScript.isAiming)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementVector,Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,toRotation,rotationSpeed);
        } 
        movementVector = input.x * movementSpeed *  cameraTransform.right + input.z * cameraTransform.forward * movementSpeed;
        playerRigidBody.velocity = new Vector3(movementVector.x, playerRigidBody.velocity.y, movementVector.z);       
    }
}
