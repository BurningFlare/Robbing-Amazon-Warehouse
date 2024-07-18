using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransmutationBase : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] private float maxMoveSpeed = 5f;
    [SerializeField] private float moveAcceleration = 0.1f;

    [Header("Health")]
    [SerializeField] private float maxHealth;
    [SerializeField] private float health;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // handles acceleration and deceleration when given a direction
    // should be called per FixedUpdate in the Player class
    public void HandleMovement(Vector2 moveDirection)
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
