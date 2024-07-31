using UnityEngine;

public class SpawnGuards : MonoBehaviour
{
    [SerializeField] GameObject GuardPrefab;
    GameObject[] patrolPoints;
    Player player;

    private void Awake()
    {
        player = FindFirstObjectByType<Player>();
        patrolPoints = getPatrolPoints();
        if (player == null)
        {
            Debug.Log("player not found, have fun with the error messages");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // spawn a guard here
        if (Input.GetKeyDown(KeyCode.G))
        {
            GuardStateManager guard = Instantiate(GuardPrefab).GetComponent<GuardStateManager>();
            // set the targetPlayer of the guard to the player
            guard.targetPlayer = player;
            // initialize guard with random patrol points
            shufflePatrolPoints();
            // random for integers is exclusive on the upper bound
            int numCopiedPatrolPoints = Random.Range(1, patrolPoints.Length + 1);
            guard.patrolPoints = new Transform[numCopiedPatrolPoints];
            for (int i = 0; i < numCopiedPatrolPoints; i++)
            {
                guard.patrolPoints[i] = patrolPoints[i].transform;
            }
        }
    }

    void shufflePatrolPoints()
    {
        int n = patrolPoints.Length;
        while (n > 1)
        {
            int k = Random.Range(0, n--);
            GameObject temp = patrolPoints[n];
            patrolPoints[n] = patrolPoints[k];
            patrolPoints[k] = temp;
        }
    }

    // draws circles around the waypoints
    private void OnDrawGizmos()
    {
        GameObject[] patrolPoints = getPatrolPoints();
        foreach (GameObject patrolPoint in patrolPoints)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(patrolPoint.transform.position, 0.1f);
        }
    }

    // gets the patrol points ONLY if they are children of a GameObject called "PatrolPoints"
    private GameObject[] getPatrolPoints()
    {
        Transform patrolPointContainer = GameObject.Find("PatrolPoints").transform;
        GameObject[] patrolPoints = new GameObject[patrolPointContainer.childCount];
        for (int i = 0; i < patrolPointContainer.childCount; i++)
        {
            patrolPoints[i] = patrolPointContainer.GetChild(i).gameObject;
        }
        return patrolPoints;
    }

}
