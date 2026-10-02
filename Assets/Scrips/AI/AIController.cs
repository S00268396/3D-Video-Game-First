using UnityEngine;
using UnityEngine.AI;

public delegate void AINavigationHandler();

[DisallowMultipleComponent]
[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIController : MonoBehaviour 
{
    //This is the AI for the Enemy
    protected NavMeshAgent Agent;
    protected Vector3? CurrentTarget;

    public event AINavigationHandler OnReachedDestination;
    public event AINavigationHandler OnDestinationChanged;
    public event AINavigationHandler OnMovementFailed;

    protected virtual void Awake()
    {
        Agent = GetComponent<NavMeshAgent>();
    }
    public bool HasReachedDestination
    {
        get
        {
            return (!Agent.pathPending && CurrentTarget.HasValue && Agent.remainingDistance <= Agent.stoppingDistance);
        }
    }
    public bool IsMoving
    {
        get
        {
            return (!Agent.isStopped && Agent.velocity.sqrMagnitude > 0.01f);
        }
    }
    protected virtual void Update()
    {
        if (!Agent.isStopped)
        {
            if (HasReachedDestination)
            {
                Agent.isStopped = true;
               CurrentTarget = null;
                OnReachedDestination?.Invoke();
            }
        }
    }
    public bool MoveTo(Vector3 Target)
    {
        bool Success = Agent.SetDestination(Target);

        if (Success)
        {
            CurrentTarget = Target;
            Agent.isStopped = false;

            OnDestinationChanged?.Invoke();
        }
        else 
        { 
            OnMovementFailed?.Invoke();
        }
        return Success;
    }
    public bool MoveToRandomNavMeshPoint(float Radius)
    {
        Vector3 RandomDirection = Random.insideUnitSphere * Radius + transform.position;

        if (NavMesh.SamplePosition(RandomDirection, out NavMeshHit Hit, Radius, NavMesh.AllAreas ))
        {
            return MoveTo(Hit.position);
        }   
        return false;
    }
    public bool TeleportTo(Vector3 Target)
    {
        return Agent.Warp(Target);
    }
    public void StopMovement()
    {
        Agent.isStopped = true;
        Agent.ResetPath();
        CurrentTarget = null;
    }
    public void PauseMovement()
    {
       Agent.isStopped = true; 
    }
    public void ResumeMovement()
    {
        if (CurrentTarget.HasValue)
        {
            Agent.isStopped = false;
        }
    }
    public void EnableAvoidance(ObstacleAvoidanceType AvoidanceType, int AvoidancePriority = 50)
    {
        Agent.obstacleAvoidanceType = AvoidanceType;
        Agent.avoidancePriority = AvoidancePriority;
    }
    public void DisableAvoidance()
    {
        Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
    }
}
