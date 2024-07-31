using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardAnimationHandler : MonoBehaviour
{
    Animator animator;
    GuardStateManager guard;
    SpriteRenderer spriteRenderer;
    [SerializeField] private float spriteFlipThreshold = 0.1f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        guard = GetComponent<GuardStateManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 guardVelocity = guard.agent.velocity;
        animator.SetFloat("Speed", guardVelocity.sqrMagnitude);
        if (guardVelocity.x < -spriteFlipThreshold)
        {
            spriteRenderer.flipX = true;
        }
        else if (guardVelocity.x > spriteFlipThreshold)
        {
            spriteRenderer.flipX = false;
        }
    }
}
