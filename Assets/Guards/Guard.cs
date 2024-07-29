using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Guard : PathfindingEnemy
{
    /*
    *
    *
    * THIS CLASS IS NO LONGER IN USE
    * refer to GuardStateManager
    *
    *
    */

    [Header("Target Player")]
    [SerializeField] private Player targetPlayer;

    [Header("Movement Speed")]
    [SerializeField] public float moveSpeed;

    NavMeshAgent agent;

    // Guard current state [Patrol, Alerted, Chase]
    public enum GuardStates { PATROL, ALERTED, CHASE }
    private GuardStates currentState;

    public Transform[] patrolPoints;
    public int targetPatrolPointIndex;
    private Vector2 targetPosition;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        currentState = GuardStates.PATROL;
        targetPatrolPointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // TODO: better pathfind, respect collisions
        switch (currentState)
        {
            case GuardStates.PATROL:
                patrol();
                break;
            case GuardStates.ALERTED:
                checkLocation();
                break;
            case GuardStates.CHASE:
                chase();
                break;
        }
    }
    void patrol()
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
    void chase()
    {
        // move towards the player
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
    }

    void checkLocation()
    {
        // move towards location slowly
        Vector2 direction = targetPosition - (Vector2)transform.position;
        direction.Normalize();
        transform.position += (Vector3)direction * moveSpeed * Time.deltaTime * 0.4f;

        // 
    }

    // Public methods to change guard state

    public override void chaseTarget(Vector2 targetPosition)
    {
        currentState = GuardStates.ALERTED;
        this.targetPosition = targetPosition;
    }

    public override void wanderTarget(Vector2 targetPosition)
    {
        throw new System.NotImplementedException();
    }

    public override void lookoutFor(string TransmutationName)
    {
        throw new System.NotImplementedException();
    }
}
