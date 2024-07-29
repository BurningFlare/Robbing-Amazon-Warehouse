using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GuardStateManager : PathfindingEnemy
{
    [Header("Target Player")]
    [SerializeField] public Player targetPlayer;

    [Header("Movement Speed")]
    [SerializeField] public float moveSpeed;

    [Header("Vision Distance")]
    [SerializeField] public float visionDistance;

    [Header("Suspicion Timer")]
    [SerializeField] public int suspicionTimer;

    [Header("Chase Duration")] // amount of time the guard will chase the player if out of LOS
    [SerializeField] public int chaseDuration;

    public NavMeshAgent agent;

    public Transform[] patrolPoints;
    public int targetPatrolPointIndex;
    private Vector2 targetPosition;
    public int suspicion;

    private GuardBaseState currentState;
    public PatrolState patrolState = new PatrolState();
    public WalkTowardsState walkTowardsState = new WalkTowardsState();
    public CheckPropsState checkPropsState = new CheckPropsState();
    public ChasePlayerState chasePlayerState = new ChasePlayerState();


    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = moveSpeed;

        targetPatrolPointIndex = 0;
        suspicion = 0;
    }
    void Start()
    {
        currentState = patrolState;
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(GuardBaseState newState)
    {
        currentState = newState;
        currentState.EnterState(this);
    }

    public override void chaseTarget(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        SwitchState(chasePlayerState);
    }

    public override void wanderTarget(Vector2 targetPosition)
    {
        this.targetPosition = targetPosition;
        SwitchState(walkTowardsState);
    }

    public override void lookoutFor(string TransmutationName)
    {
        throw new System.NotImplementedException();
    }
}
