using UnityEngine;

public interface IDamageable
{
    void ApplyDamage(float DamageAmount);
    bool IsAlive{ get;  }
}
