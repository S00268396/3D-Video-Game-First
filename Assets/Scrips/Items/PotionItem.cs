using System;
using System.ComponentModel;
using UnityEngine;
[Serializable]
public enum PotionType {Health}

[CreateAssetMenu(menuName = "Items/PotionItem")]
public class PotionItem : Item
{   
    [SerializeField]
    private int LivesAdd = 1;//How many lives this potion restores

    //Called when the player uses the item
    public override void Use(GameObject User)
    {
        //Get the PlayerHealth component from the User GameObject. 
        PlayerHealth playerHealth = User.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.AddLife(LivesAdd); //add lives to it if the component exists.
        }
    }
}
