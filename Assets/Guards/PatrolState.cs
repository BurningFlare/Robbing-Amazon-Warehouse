using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshPlus.Extensions;
using UnityEngine;
using UnityEngine.InputSystem.Interactions;

public class PatrolState : GuardBaseState
{
    public override void EnterState(GuardStateManager guard)
    {
        // navigate to the closest patrol point >= current patrol point

        // For now, we're using euclidian distance, but I would like to use navMesh distance
        int closestPatrolPointIndex = guard.targetPatrolPointIndex;

        for (int i = 0; i < guard.patrolPoints.Length; i++)
        {
            if (Vector2.Distance(guard.transform.position, guard.patrolPoints[i].position) < Vector2.Distance(guard.transform.position, guard.patrolPoints[closestPatrolPointIndex].position))
            {
                closestPatrolPointIndex = i;
            }
        }

        guard.targetPatrolPointIndex = closestPatrolPointIndex;
    }

    public override void UpdateState(GuardStateManager guard)
    {
        RaycastHit2D ray = Physics2D.Raycast(guard.transform.position, guard.targetPlayer.currentTransmutation.transform.position - guard.transform.position, guard.visionDistance);
        if (ray.collider != null && ray.collider.gameObject.CompareTag("Player") || guard.suspicion > 0)
        {
            // increment suspicion
            guard.suspicion ++;

            if (guard.suspicion > guard.suspicionTimer)
            {
                guard.suspicion = guard.suspicionTimer;
                //guard.SwitchState(guard.chasePlayerState);
                Debug.Log("chase!!");
            }
        }
        else
        {
            // decrease guard suspicion
            guard.suspicion = guard.suspicion > 0 ? guard.suspicion - 1 : 0;

            moveTowardPatrolPoint(guard);
        }

    }

    public void moveTowardPatrolPoint(GuardStateManager guard) 
    {
        // move towards the next patrol point
        guard.agent.SetDestination(guard.patrolPoints[guard.targetPatrolPointIndex].position);

        // check if we're close enough to the patrol point
        if (Vector2.Distance(guard.transform.position, guard.patrolPoints[guard.targetPatrolPointIndex].position) < 0.1f)
        {
            guard.targetPatrolPointIndex = (guard.targetPatrolPointIndex + 1) % guard.patrolPoints.Length;
        }

        // check if we have LoS to the player
    }

}
