using UnityEngine;

public class EnemyFireBall : EnemyController, IDamageable
{
    [SerializeField]
    private GameObject FireBallPre;
    [SerializeField]
    private Transform FireBallPoint;
    [SerializeField]
    private float CoolDown = 2f;
    [SerializeField]
    private float FireBallSpeed = 5f;
    [SerializeField]
    private float FireBallDamage = 1f; 
    [SerializeField]
    private float time = 5f;
    [SerializeField]
    private float Health = 1f;


    private float LastFireBallTime = 0f;
    private float distancePlayer;

    public bool IsAlive
    {
        get { return Health > 0; }
    }

    private void Start()
    {
        if (CurrentNode != null)
        {
            transform.position = CurrentNode.transform.position;
        }

    }
    protected override void Update()
    {
        base.Update();       

        if (PlayerTransform != null)
        {
            distancePlayer = Vector3.Distance(transform.position, PlayerTransform.position);

            if (distancePlayer <= DectectionRange && Time.time - LastFireBallTime >= CoolDown)
            {
                ShootAtPlayer();
                LastFireBallTime = Time.time;
            }
        }
       
    }

    private void ShootAtPlayer()
    {
       GameObject FireBallIstant = Instantiate(FireBallPre, FireBallPoint.position, FireBallPoint.rotation);

        FireBallIstant.transform.LookAt(PlayerTransform.position);
        FireBall FireBallScript = FireBallIstant.GetComponent<FireBall>();

        FireBallScript.Speed = FireBallSpeed;
        FireBallScript.Damage = FireBallDamage;
        Destroy(FireBallIstant, time);

    }
    public void ApplyDamage(float DamageAmount)
    {
        Health -= DamageAmount;
        if (!IsAlive)
        {
            Destroy(gameObject);
        }
    }
}
