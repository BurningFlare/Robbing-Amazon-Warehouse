using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerBase : MonoBehaviour
{
    [Header("Information")]
    [SerializeField] private string propName;

    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float moveAcceleration = 0.1f;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    // These two should be guaranteed to be normalized
    private Vector2 moveDirection;
    private Vector2 mostRecentMoveDirection;

    // Components
    private Interactor interactor;
    private Rigidbody2D rigidBody;
    private Camera mainCamera;
    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        interactor = GetComponent<Interactor>();
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        playerInputHandler = GetComponent<PlayerInputHandler>();
    }

    // Start is called before the first frame update
    void Start()
    {
        PrintDebug();
    }

    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        UpdateInteractorPosition();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void PrintDebug()
    {
        Debug.Log("Name: " + propName + "\nHealth: " + health + "\nmoveSpeed: " + maxMoveSpeed);
    }

    void GetMovementInput()
    {
        moveDirection = playerInputHandler.MoveInput;
        moveDirection.Normalize();
        if (moveDirection.magnitude >= 0.1)
        {
            mostRecentMoveDirection = moveDirection;
        }
    }
    
    void UpdateInteractorPosition()
    {
        interactor.interactionPoint.localPosition = mostRecentMoveDirection;
    }

    // handles acceleration, deceleration, and movement inputs
    void HandleMovement()
    {
        // Handle Acceleration
        // if we are holding an input, accelerate in that direction
        if (moveDirection != Vector2.zero)
        {
            Vector2 deltaVelocityIncrease = moveDirection * maxMoveSpeed / moveAcceleration * Time.fixedDeltaTime;
            rigidBody.velocity += deltaVelocityIncrease;
            // if we are faster than our max speed, clamp our speed
            // technically since we only check for this when the player is moving at some point this means the velocity is uncapped when the player isn't holding a movement button
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxMoveSpeed); 
        }
    }
}