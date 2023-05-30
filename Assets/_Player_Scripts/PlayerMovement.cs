using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    CharacterController controller;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraTransform;
    [SerializeField] float playerSpeed = 2.0f;
    [SerializeField] float jumpHeight = 1.0f;
    [SerializeField] float gravityValue = -9.81f;
    [SerializeField] float rotationSpeed = 4f;
    [SerializeField] InputActionReference jumpControl; 
    PlayerControls playerControls;
    InputAction movementControl;
    #region Input Setup
    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {   
        movementControl = playerControls.Player.Movement;
        movementControl.Enable();
        jumpControl.action.Enable();
    }
    private void OnDisable()
    {
        movementControl.Disable();
        jumpControl.action.Disable();
    }
    #endregion
    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        PlayerState.Instance.Aiming = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if(ControlsManager.Instance.IsPauseButtonDown)
        {
            PlayerState.Instance.Paused = !PlayerState.Instance.Paused;
            CanvasManager.Instance.PauseCanvas.enabled = true;
        }
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movementDirection = movementControl.ReadValue<Vector2>();
        Vector3 move = new Vector3(movementDirection.x,0,movementDirection.y);
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;

        controller.Move(move * Time.deltaTime * playerSpeed);

        if (jumpControl.action.triggered)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        if(movementDirection != Vector2.zero && !PlayerState.Instance.Aiming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move,Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}