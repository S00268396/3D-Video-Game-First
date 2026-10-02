using UnityEngine;

public class DebugDamageable : MonoBehaviour, IDamageable
{
  private float Health = 1;

    public bool IsAlive
    {
        get { return Health > 0; }
    }   
    public void ApplyDamage(float DamageAmount)
    {
        Health -= DamageAmount;

        if (!IsAlive)
        {
            Destroy(gameObject);
            Debug.Log("DebugDamageable destroyed");
        }
    }
}
