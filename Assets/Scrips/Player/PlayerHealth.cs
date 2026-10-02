using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Player Health Settings: ")]
    [SerializeField]
    private int NumberOfLife = 5; //Hoew many lives the player has before dying
    [SerializeField]
    private TMP_Text HealthNumber; //Text component to display the player's current health on the UI

    //Reference to the CheckPoint script
    private PlayerCheckpoint playerCheckpoint;
    
    //Returns true if the player has more than 0 lives
    public bool IsAlive => NumberOfLife > 0;

    public void Awake()
    { 
        UpdateText();
    }

    public void ApplyDamage(float Damage)
    {
        
        Debug.Log("Player was attack");

        UpdateText();

        //Get the reference to the CheckPoint script
        playerCheckpoint = GetComponent<PlayerCheckpoint>();

        
        if ( IsAlive)
        {//Reduce the player's health by 1
            NumberOfLife--;

            //If the player has a checkpoint, respawn at the checkpoint instead of dying
            if (playerCheckpoint != null)
            {
                playerCheckpoint.RespawnAtCheckPoint();
            }
        }
        else if (!IsAlive)
        {
            //If no lives are left, the player dies
            Die();
        }

    }

    //Updates the text component to show the current number of lives
    private void UpdateText()
    {
       if(HealthNumber != null) 
       {
            HealthNumber.SetText($"Live: {NumberOfLife}");
       }
    }

    //Method to add lives to the player, default is 1 life
    public void AddLife(int live = 1)
    {
        NumberOfLife += live;
        UpdateText();
    }
    
    //Method to handle the player's death
    public void Die()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //Destroy(gameObject);
        Debug.Log("Player destroyed");
    }
}

