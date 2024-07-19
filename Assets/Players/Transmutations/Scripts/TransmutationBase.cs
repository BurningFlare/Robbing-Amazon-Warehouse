using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TransmutationBase : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] protected float maxMoveSpeed = 5f;
    [SerializeField] protected float moveAcceleration = 0.1f;

    [Header("Health")]
    [SerializeField] protected float maxHealth;
    public float health;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        health = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
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

    public void HandleJump(bool jump)
    {
        // TODO check if player is on the ground and then figure out some vertical velocity bullshit and change the collider layers and something
    }
}
