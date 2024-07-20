using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : PathfindingEnemy
{

    [Header("Target Player")]
    [SerializeField] private Player targetPlayer;

    [Header("Movement Speed")]
    [SerializeField] private float moveSpeed;

    // patrol, or check for player?
    private bool isAlerted = true;

    public Transform[] patrolPoints;
    public int targetPatrolPointIndex = 0;

    private void Awake()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: better pathfind, respect collisions
        if (isAlerted)
        {
            // move towards the player
            Vector2 direction = targetPlayer.currentTransmutation.transform.position - transform.position;
            direction.Normalize();
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            // move towards the next patrol point
            Vector2 direction = patrolPoints[targetPatrolPointIndex].position - transform.position;
            direction.Normalize();
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;

            // check if we're close enough to the patrol point
            if (Vector2.Distance(transform.position, patrolPoints[targetPatrolPointIndex].position) < 0.1f)
            {
                // move to the next patrol point
                targetPatrolPointIndex = (targetPatrolPointIndex + 1) % patrolPoints.Length;
            }
        }

    }
}
