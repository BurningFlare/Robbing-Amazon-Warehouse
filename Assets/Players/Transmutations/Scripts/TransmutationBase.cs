using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]

public class TransmutationBase : MonoBehaviour {

    [Header("Movement")]
    [SerializeField] protected float maxMoveSpeed = 5f;
    [SerializeField] protected float moveAcceleration = 0.1f;
    [SerializeField] protected float moveSpeedDropoffWithWeight = 0.03f;
    [SerializeField] protected float minimumMoveFactor = 5f;

    [Header("Health")]
    [SerializeField] public float maxHealth = 3;
    [SerializeField] public float health = 3;

    [Header("Interaction")]
    [SerializeField] protected float interactionDist = 0.5f; // how far ahead to place the interaction point from the player while moving

    // Components
    protected Interactor interactor;
    protected Rigidbody2D rigidBody;
    protected Vector2 mostRecentMoveDirection = Vector2.zero;

    protected virtual void Awake()
    {
        interactor = transform.parent.GetComponent<Interactor>();
        health = maxHealth;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    // handles acceleration and deceleration when given a direction
    // should be called per FixedUpdate in the Player class
    public virtual void HandleMovement(Vector2 moveDirection)
    {
        Accelerate(moveDirection);
        UpdateInteractorPosition();
    }

    protected virtual void Accelerate(Vector2 moveDirection)
    {
        // Handle Acceleration
        // if we are holding an input, accelerate in that direction
        if (moveDirection != Vector2.zero)
        {
            // this section modifies the movement speed with the weight of the player
            float weightSlowFactor = GameManager.Instance.player.inventory.totalWeight * moveSpeedDropoffWithWeight;
            float accelerationFactor = maxMoveSpeed / moveAcceleration;
            float moveFactor = accelerationFactor - weightSlowFactor;
            moveFactor = moveFactor < minimumMoveFactor ? minimumMoveFactor : moveFactor;

            // apply the calculated velocity to the player
            Vector2 deltaVelocityIncrease = moveDirection * moveFactor * Time.fixedDeltaTime;
            rigidBody.velocity += deltaVelocityIncrease;
            // if we are faster than our max speed, clamp our speed
            // technically since we only check for this when the player is moving at some point this means the velocity is uncapped when the player isn't holding a movement button
            rigidBody.velocity = Vector2.ClampMagnitude(rigidBody.velocity, maxMoveSpeed);
        }
        if (moveDirection.sqrMagnitude > 0.01)
        {
            mostRecentMoveDirection = moveDirection;
        }
    }

    protected void UpdateInteractorPosition()
    {
        interactor.interactionPoint.localPosition = mostRecentMoveDirection * interactionDist;
    }

    public void HandleJump(bool jump)
    {
        // TODO check if player is on the ground and then figure out some vertical velocity bullshit and change the collider layers and something
    }

    public void Damage(float damageAmount)
    {
        health -= damageAmount;
        if (health < 0)
        {
            // honestly how this works is a bit messy, I'm calling the GameManager to invoke the event that I want to invoke. This is because this class is meant to be temporary and I don't wanna hold the event here
            // it's kinda equivalent to using a delegate... although I guess I can't forcibly remove all of the bindings if I wanted to
            GameManager.Instance.handlePlayerDeath();
        }
    }
}
