using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Used for enemies that can pathfind and chase the player
abstract public class PathfindingEnemy : EnemyBase
{
    abstract public void chaseTarget(Vector2 targetPosition);
    abstract public void wanderTarget(Vector2 targetPosition);
}
