using UnityEngine;

public class Patform : MonoBehaviour
{
    [Header("Patform Settings")]
    //Current node that the platform is moving towards
    [SerializeField]
    private PathNode CurrentNode;

    //Speed at which the platform moves towards the target node
    [SerializeField]
    private float speed = 2f;

    //Control whether the platform is active and should move or not
    protected bool IsActive = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        transform.position = CurrentNode.transform.position;
    }

    protected virtual void Update()
    {
        
        if (CurrentNode != null)
        {
            // Move towards the target node
            transform.position = Vector3.MoveTowards(transform.position, CurrentNode.transform.position, speed * Time.deltaTime);

            // Check if the platform has reached the target node before moving to the next one.
            if (Vector3.Distance(transform.position, CurrentNode.transform.position) < 0.1f)
            {
                // Move to the next node
                CurrentNode = CurrentNode.GetNextNode(); ;
            }
        }
    }

    //Current movement speed of the platform, calculated as the change in position over time.
    public Vector3 Velocity { get; private set; }

    private Vector3 lastPosition;

    private void LateUpdate()
    {
        // Calculate the velocity of the platform based on the change in position over time.
        Velocity = (transform.position - lastPosition) / Time.deltaTime;
        lastPosition = transform.position;
    }

    //Activate the platform, allowing it to move towards its target node.
    public virtual void Active() 
    { 
        IsActive = true;
    }

    //Stop Platform movement
    public virtual void NotActive()
    {
        IsActive = false;
    }
}
