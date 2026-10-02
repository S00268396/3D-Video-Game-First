using UnityEngine;

public interface IBreakeable
{
    //Applies damage to the object.
    void ApplyDamage(float DamageAmount);

    //Returns true if the object can be broken, false otherwise.
    bool IsBreaking { get; }

}
