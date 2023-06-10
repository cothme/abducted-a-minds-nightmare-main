using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{   
    [SerializeField] GameObject rifle;
    [SerializeField] GameObject shotgun;
    [SerializeField] GameObject pistol;
    [SerializeField] GameObject knife;
    [SerializeField] GameObject healthkit;
    [SerializeField] GameObject oxygenkit;
    [SerializeField] GameObject ammo;
    [SerializeField] GameObject mask;
    [SerializeField] GameObject flashlight;
    [SerializeField] GameObject uvflashlight;
    Animator animator;
    float playerSpeed = 6.0f;
    float gravityValue = -9.81f;
    float rotationSpeed = 4f;
    CharacterController controller;
    Vector3 playerVelocity;
    bool groundedPlayer;
    Transform cameraTransform;
    PlayerControls playerControls;
    InputAction movementControl;
    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {   
        movementControl = playerControls.Player.Movement;
        movementControl.Enable();
    }
    private void OnDisable()
    {
        movementControl.Disable();
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
        cameraTransform = Camera.main.transform;
        PlayerState.Instance.Aiming = false;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (Input.GetKey(KeyCode.LeftShift) && !PlayerState.Instance.Aiming && !PlayerState.Instance.Reloading)
        {
            PlayerState.Instance.Running = true;
            playerSpeed += 2f;
            if (playerSpeed >= 10f)
            {
                playerSpeed = 20f;
            }
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            PlayerState.Instance.Running = false;
            playerSpeed = 6f;
        }
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        Vector2 movementDirection = movementControl.ReadValue<Vector2>();

        Vector3 move = new Vector3(movementDirection.x, 0, movementDirection.y);
        PlayerState.Instance.MovingX = move.x;
        PlayerState.Instance.MovingZ = move.z;
        move = cameraTransform.forward * move.z + cameraTransform.right * move.x;
        move.y = 0;

        controller.Move(move * Time.deltaTime * playerSpeed);

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        RotateBasedOnWASD(movementDirection, move);
    }
    public void RotateBasedOnWASD(Vector2 movementDirection, Vector3 move)
    {
        if (movementDirection != Vector2.zero && !PlayerState.Instance.Aiming)
        {
            Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}