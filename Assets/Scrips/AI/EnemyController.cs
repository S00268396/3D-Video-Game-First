using Unity.VisualScripting;
using UnityEngine;

public enum AISate
{
    Patrol,
    Chase,
    Attack
}
public class EnemyController : AIController
{
    [SerializeField]
    public Transform PlayerTransform;
    [SerializeField]
    private float ResumeFollowDistance = 3f;
    [SerializeField]
    public float DectectionRange = 2.6f;
    [SerializeField]
    private float LessDistance = 1.5f;

    private bool IsFollowing;
    private AISate CurrentState = AISate.Patrol;
    private Vector3 LastPlayerPosition;

    //Attack Player
    [SerializeField]
    private int AttackDamage = 1;
    [SerializeField]
    private float AttackCoolDown = 1.5f;
    private float LastAttackPlayer = 0f;

    //Player Health - Need to create a Player Health class in the player

    //PathNode
    [SerializeField]
    public PathNode CurrentNode;
    [SerializeField]
    private float Speed = 2f;
    public Vector3 Velocity {  get; private set; }
    private Vector3 LastNode;


    private void Start()
    {
        transform.position = CurrentNode.transform.position;
    }

    protected override void Update()
    {
        base.Update();

        if (PlayerTransform != null)
        {            
            float DistanceToPlayer = Vector3.Distance(transform.position, PlayerTransform.position);

            //This handle the follow part so maybe use that and also create two more, HandleAttack and HandleChase. Oh and use the switch method
            //HandleFollowThreshold(DistanceToPlayer);

            switch (CurrentState)
            {
                case AISate.Patrol:
                    HandlePatrol(DistanceToPlayer); 
                    break;
                case AISate.Chase:
                    HandleChase(DistanceToPlayer);
                    break;
                case AISate.Attack:
                    HandleAttack(DistanceToPlayer);
                    break;
            }
        }
          
        
           
    }

    private void LateUpdate()
    {
        Velocity = (transform.position - LastNode) / Time.deltaTime;
        LastNode = transform.position;
    }


    private void HandlePatrol(float PlayDis)
    {
        if (PlayDis <= DectectionRange)
        {
            StartChase();
        }
        else
        {
            KeepPatrol();
        }
    }

    private void HandleChase(float PlayDis)
    {
        if (PlayDis <= LessDistance)
        {
            AttackPlayer();
            return;
        }
        else if (PlayDis > DectectionRange)
        {
            KeepPatrol();
            return;
        }

        MoveTo(PlayerTransform.position);
        LastPlayerPosition = PlayerTransform.position;
    }
    private void HandleAttack(float PlayDis)
    {
        if (PlayDis > LessDistance)
        {
            StartChase();
        }
        else
        {
            AttackPlayer();
        }

    }
   
    private void StartChase()
    {
        if (CurrentState == AISate.Chase)
        {
            return;
        }

        CurrentState = AISate.Chase;
        Debug.Log("Al switch to Chase mode");

        MoveTo(PlayerTransform.position);
        LastPlayerPosition = PlayerTransform.position;
    }
    //private void StopChase()
    //{
    //    if (CurrentState == AISate.Patrol)
    //    {
    //        return;
    //    }
    //    CurrentState = AISate.Patrol;
    //    Debug.Log("Al switch to Patrol mode");

    //    KeepPatrol();
      
    //}
    private void KeepPatrol()
    {
        CurrentState = AISate.Patrol;

        if (CurrentNode != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, CurrentNode.transform.position, Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, CurrentNode.transform.position) < 0.1f)
            {
                CurrentNode = CurrentNode.GetNextNode();
            }
        }
    }

    private void AttackPlayer()
    {
        if (CurrentState == AISate.Attack)
        {
            return;
        }
        CurrentState = AISate.Attack;
        Debug.Log("Al switch to Attack mode");
        if (Time.time - LastAttackPlayer >= AttackCoolDown)
        {
            IDamageable Damage = PlayerTransform.GetComponent<IDamageable>();

            if (Damage != null && Damage.IsAlive) 
            {
                Damage.ApplyDamage(AttackDamage);
                Debug.Log("Enemy attack the Player");
            }

            LastAttackPlayer = Time.time;
        } 
    }
    

    //private void HandleFollowThreshold(float DistanceToPlayer)
    //{    //I think I need to change this so it have the stopfollowDistance, UpdateFollowPlayer, StartFollowing

    //    if (!IsFollowing)
    //    {
    //        if (DistanceToPlayer > ResumeFollowDistance)
    //        {
    //            IsFollowing = true;
    //            LastPlayerPosition = PlayerTransform.position;
    //            MoveTo(PlayerTransform.position);
    //        }
    //    }
    //    else
    //    {
    //        float PlayerMoveDelta = Vector3.Distance(PlayerTransform.position, LastPlayerPosition);

    //        if (PlayerMoveDelta > Agent.stoppingDistance)
    //        {
    //            LastPlayerPosition = PlayerTransform.position;
    //            MoveTo(PlayerTransform.position);
    //        }
    //    }
    //}
    private void OnEnable()
    {
        OnReachedDestination += CompanionController_OnReachedDestination;
    }
    private void OnDisable()
    {
        OnReachedDestination -= CompanionController_OnReachedDestination;
    }
    private void CompanionController_OnReachedDestination()
    {
        IsFollowing = false;
    }

    private void StartFollowing()
    {

    }
    private void StopFollowing()
    {

    }
    private void UpdateFollowPlayer()
    {

    }
}
