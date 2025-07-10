using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.HID;
using UnityEngine.UI;

public class CharacterControls : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField]
    private int moveSpeed;
    [SerializeField]
    private int JumpHeight;
    private Controls controls;
    private Vector2 lookInput;
    public Transform playerCamera;
    public Vector2 moveInput;
    private CharacterController characterController;
    private Vector3 velocity;
    private float gravity = -9.8f;
    [SerializeField]
    private float lookSpeed;
    private float verticalLookRotation = 0f;
    [SerializeField]
    private int Range;
    public GameObject projectilePrefab;

    //Interaction 
    public LayerMask InteractLayer;


    //Pickup Items and spells
    private bool HasFireBall, HasLightning, SuperBoots, hasHotFeet, HasDoubleDamage, HasBoxingGloves, HasBubbles;

    [SerializeField]
    private int PlayerIndex;

    

    private void OnEnable()
    {
        if(PlayerIndex == 1)
        {
            controls = new Controls();
            controls.Player.Enable();
            controls.Player.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Player.LookAround.canceled += ctx => lookInput = Vector2.zero;

            controls.Player.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
            controls.Player.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

            controls.Player.Interact.performed += ctx => Interact();
            controls.Player.Jump.performed += ctx => Jump();
            controls.Player.Attack.performed += ctx => Attack();

        }
        else if (PlayerIndex == 2)
        {
            controls = new Controls();
            controls.Player2.Enable();
            controls.Player2.LookAround.performed += ctx => lookInput = ctx.ReadValue<Vector2>();
            controls.Player2.LookAround.canceled += ctx => lookInput = Vector2.zero;

            controls.Player2.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>(); // Update moveInput when movement input is performed
            controls.Player2.Movement.canceled += ctx => moveInput = Vector2.zero; // Reset moveInput when movement input is canceled

            controls.Player2.Interact.performed += ctx => Interact();
            controls.Player2.Jump.performed += ctx => Jump();
            controls.Player2.Attack.performed += ctx => Attack();
        }


    }

    private void Update()
    {
        ApplyGravity();
        Move();
        LookAround();
    }

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Interact()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;


        /*if (Physics.Raycast(ray, out hit, PickupRange, InteractLayer))
        {
        }*/
    }

    void Attack()
    {
        Ray ray = new Ray(playerCamera.position, playerCamera.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Range))
        {
            GameObject Projectile = Instantiate(projectilePrefab, playerCamera.position, Quaternion.identity);
            ProjectileController ProjectileDestination = Projectile.GetComponent<ProjectileController>();
            ProjectileDestination.hitPoint = hit.point;
        }
    }
    
    void Jump()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        if (Physics.Raycast(ray, out RaycastHit hit, 1))
        {
            velocity.y = Mathf.Sqrt(JumpHeight * -2f * gravity);
        }
    }

    void Move()
    {
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y);

        move = transform.TransformDirection(move);

        characterController.Move(move * moveSpeed * Time.deltaTime);
    }

    public void ApplyGravity()
    {
        if (characterController.isGrounded && velocity.y < 0)
        {
            velocity.y = -0.5f; // Small value to keep the player grounded
        }
        velocity.y += gravity * Time.deltaTime; // Apply gravity to the velocity

        characterController.Move(velocity * Time.deltaTime); // Apply the velocity to the character

    }

    public void LookAround()
    {
        /// Get horizontal and vertical look inputs and adjust based on sensitivity
        float LookX = lookInput.x * lookSpeed;
        float LookY = lookInput.y * lookSpeed;

        // Horizontal rotation: Rotate the player object around the y-axis
        transform.Rotate(0, LookX, 0);

        // Vertical rotation: Adjust the vertical look rotation and clamp it to prevent flipping
        verticalLookRotation -= LookY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -90, 60);

        // Apply the clamped vertical rotation to the player camera
        playerCamera.localEulerAngles = new Vector3(verticalLookRotation, 0, 0);
    }

}
