using UnityEngine;

public class PlayerCheckpoint : MonoBehaviour
{
    //Store the last saved checkpoint position
    private Vector3 LastCheckPoint;

    private void Start()
    {
        //Default checkpoint to the player's starting position
        LastCheckPoint = transform.position;
    }

    private void Update()
    {
        //Press R to respawn at the last checkpoint for testing purposes
        if (UnityEngine.InputSystem.Keyboard.current.rKey.wasPressedThisFrame)
        {
            RespawnAtCheckPoint();
        }
    }

    //Method to update the checkpoint position when the player reaches a new checkpoint
    public void SetNewCheckPoint(Transform NewCheckPoint)
    {
        LastCheckPoint = NewCheckPoint.position;
        Debug.Log("The CheckPoint was update");
    }

    //Method to respawn the player at the last checkpoint position
    public void RespawnAtCheckPoint()
    {
        CharacterController controller = GetComponent<CharacterController>();
        controller.enabled = false;
        transform.position = LastCheckPoint;
        controller.enabled = true;

        Debug.Log("Player respawn at the CheckPoint");
    }
}
