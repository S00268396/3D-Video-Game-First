using JetBrains.Annotations;
using System;
using UnityEngine;
[Serializable]
public enum WeaponType {FireBall}

[CreateAssetMenu(menuName = "Items/Weapon")]
public class WeaponItem : Item
{
    [SerializeField]
    public int PlusFireBall = 5; //How many fireballs this weapon adds to the player when used

    public WeaponType WeaponType;

    //Called when the player uses the item
    public override void Use(GameObject User)
    {
        //Get the PlayerShootFireBall component from the User GameObject.
        PlayerShootFireBall Shoot = User.GetComponent<PlayerShootFireBall>();

        //If the component exists, add the PlusFireBall amount to the player's fireball count.
        if (Shoot != null)
        {
            Shoot.AddFireBall(PlusFireBall);
        }
    }

}
