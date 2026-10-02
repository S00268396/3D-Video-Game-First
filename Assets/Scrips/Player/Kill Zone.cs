using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Check if the colliding object has the "Player" tag
        if (other.CompareTag("Player"))
        {
            //Get the PlayerHealth component from the colliding object
            PlayerHealth health = other.GetComponent<PlayerHealth>();

            //If the Health component exists, apply damage to the player
            if (health != null)
            {
                health.ApplyDamage(1);
            }
        }
    }
}
