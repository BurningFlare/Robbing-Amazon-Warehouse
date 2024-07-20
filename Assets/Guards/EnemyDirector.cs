using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyDirector : MonoBehaviour
{
    static List<EnemyBase> enemyList = new List<EnemyBase>();
    static EnemyDirector instance;
    static Vector3 reportedPredictedLocation;
    static string reportedTransmutationName;

    static List<PathfindingEnemy> currentlyChasing = new List<PathfindingEnemy>();
    const int NUM_GUARDS_TO_ALERT = 3;
    private void Awake()
    {
        // Only want one instance of enemy director
        if (instance == null) { instance = this; }
        else { Destroy(this); }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public static void reportPredictedLocation(Vector3 location, string transmutationName)
    {
        reportedPredictedLocation = location;
        reportedTransmutationName = transmutationName;
        alertEnemies();
        alertNearbyPathfindersToChase();
    }

    // This method should be used when reporting that the target was not found at the predicted location
    public static void reportTargetLost()
    {
        
    }

    // This method should be used when an enemy stops chasing the target
    public static void stoppedChasing(PathfindingEnemy enemy)
    {
        currentlyChasing.Remove(enemy);
    }

    // should be called on start for every enemy
    public static bool registerEnemy(EnemyBase enemy) {
        enemyList.Add(enemy);
        return true;
    }

    // this function tells all enemies to be on the lookout for the transmutation
    static void alertEnemies()
    {
        foreach (EnemyBase enemy in enemyList) { enemy.lookoutFor(reportedTransmutationName); }
    }

    // this tells guards to chase
    static void alertNearbyPathfindersToChase()
    {
        foreach(PathfindingEnemy enemy in findNearbyPathfinders(NUM_GUARDS_TO_ALERT))
        {
            enemy.chaseTarget(reportedPredictedLocation);
        }
    }


    // this is not guaranteed to return a list of countEnemies enemies that pathfind
    static List<PathfindingEnemy> findNearbyPathfinders(int countEnemies)
    {
        List<PathfindingEnemy> closestPathfinders = new List<PathfindingEnemy>();

        // I need to do this check cuz the for loop below will most likely give at least 1 enemy
        if (countEnemies <= 0) { return closestPathfinders; } // bro why are you asking for 0 enemies

        // sort the enemies by distance to the target
        List<EnemyBase> enemiesSortedByDistToTarget = enemyList.OrderBy(enemy => (enemy.transform.position - reportedPredictedLocation).sqrMagnitude).ToList();

        // get the first countEnemies enemies that are pathfinders
        foreach (EnemyBase enemy in enemiesSortedByDistToTarget)
        {
            PathfindingEnemy pathfindingEnemy = enemy as PathfindingEnemy;
            if (pathfindingEnemy != null)
            {
                closestPathfinders.Add(pathfindingEnemy);
                if (closestPathfinders.Count >= countEnemies)
                {
                    break;
                }
            }
        }
        return closestPathfinders;
    }
}