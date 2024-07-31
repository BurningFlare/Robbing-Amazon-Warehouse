using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DefaultPlayerTransmutation : TransmutationBase
{
    protected Animator animator;
    protected SpriteRenderer spriteRenderer;

    private new void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    protected void flipSpriteXFromMoveDir(float direction)
    {
        if (direction < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    public override void HandleMovement(Vector2 moveDirection)
    {
        Accelerate(moveDirection);
        UpdateInteractorPosition();
        flipSpriteXFromMoveDir(moveDirection.x);
        animator.SetFloat("Speed", moveDirection.sqrMagnitude);
    }
}
