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
    // amount of time it takes to accelerate from 0 to maxMoveSpeed
    [SerializeField] private float accelerationTime = 0.1f;
    // amount of time it takes to decelerate from maxMoveSpeed to 0;
    [SerializeField] private float decelerationTime = 0.1f;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    private Vector2 moveDirection;
    // what percentage of the max speed we are going

    private Rigidbody2D rigidBody;
    private Camera mainCamera;
    private PlayerInputHandler playerInputHandler;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        mainCamera = Camera.main;
        playerInputHandler = GetComponent<PlayerInputHandler>();

        // change accel and decel time to not cause divide by 0 error
        // however, this may still seems to not work so whatever
        if (accelerationTime <= 0.01)
        {
            Debug.LogWarning("your player acceleration time is close to 0, which may not work well");
            accelerationTime = 0.01f;
        }
        if (decelerationTime <= 0.01)
        {
            Debug.LogWarning("your player deceleration time is close to 0, which may not work well");
            decelerationTime = 0.01f;
        }
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
    }
    
    // handles acceleration, deceleration, and movement inputs
    void HandleMovement()
    {
        // if we aren't holding an input, decelerate the player
        if (moveDirection == Vector2.zero)
        {
            Vector2 newVelocity = rigidBody.velocity - rigidBody.velocity * maxMoveSpeed / decelerationTime * Time.fixedDeltaTime;
            // if we would decelerate to going to the other direction, set our velocity to 0
            if (Vector2.Dot(rigidBody.velocity, newVelocity) < 0)
            {
                moveDirection = Vector2.zero;
            }
            else
            {
                rigidBody.velocity = newVelocity;
            }
        }
        else
        {
            // if we are holding an input, accelerate the player in that direction
            // if we are trying to go in the opposite direction, reduce the effect of current velocity
            Vector2 velocityIncrease = moveDirection * maxMoveSpeed / accelerationTime * Time.fixedDeltaTime;
            if (Vector2.Angle(moveDirection, rigidBody.velocity) > 40)
            {
                rigidBody.velocity -= rigidBody.velocity * maxMoveSpeed / decelerationTime * Time.fixedDeltaTime;
            }
            rigidBody.velocity += velocityIncrease;
            // if we are faster than our max speed, clamp our speed
            // technically since we only check for this when the player is moving at some point this means the velocity is uncapped when the player isn't holding a movement button
            if (rigidBody.velocity.sqrMagnitude > maxMoveSpeed * maxMoveSpeed)
            {
                rigidBody.velocity = (rigidBody.velocity + velocityIncrease).normalized * maxMoveSpeed;
            }
        }
    }
}