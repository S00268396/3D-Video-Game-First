using UnityEngine;

public class FireBall : MonoBehaviour
{
    //The speed of the fireball.
    [SerializeField]
    public float Speed = 10f;
    [SerializeField]
    private  float time = 5f;//the time it takes to destroy itself,
    [SerializeField]
    public float Damage = 25f; //the amount of damage the fireball deals when it hits a target,
    [SerializeField]
    private LayerMask EnemyFireBallLayer;//the layer mask for the fireball to interact with
    [SerializeField]
    private LayerMask PlayerLayer;//the layer mask for the player
    [SerializeField]
    private GameObject FireBallObject;//Reference to the FireBall GameObject
    
    private void Start()
    {
        //Destroy FireBall after Set time
        Destroy(FireBallObject, time);

        //Set the collider to be a trigger so that it can detect collisions without causing physical interactions.
        if (TryGetComponent<Collider>(out Collider col))
        {
            col.isTrigger = true;
        }
    }

    private void Update()
    {
        //Move the fireball forward based on its speed and the time elapsed since the last frame.
        transform.position += transform.forward * Speed * Time.deltaTime;
    }   

    private void OnTriggerEnter(Collider other)
    {
        //Check if the layer of the collided object is in a target Layer (EnemyFireBallLayer or PlayerLayer)
        int Layer = other.gameObject.layer;
        bool IsTargeLayer = ((EnemyFireBallLayer.value | PlayerLayer.value) & (1 << Layer)) != 0;
        
        //If the collided object is not in a target layer, return without doing anything.
        if (!IsTargeLayer)
        {
            return;
        }

        //Try to deal damage if the collided is in a target layer and has the IDamageable component, then destroy the fireball.
        if (other.TryGetComponent<IDamageable>(out IDamageable damageable))
        {
            if (damageable.IsAlive)
            {
                damageable.ApplyDamage(Damage);
            }
        }
        Destroy(gameObject);
    }
}
