using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShootFireBall : MonoBehaviour
{
   [SerializeField]
    private GameObject FireBallPrefab; //Fireball object that will be spawned
    [SerializeField]
    private Transform FireBallAttachPoint; //Point where the fireball is spawned from

    [SerializeField]
    private int FireBallCount = 0; //How many fireballs the player currently have
    [SerializeField]
    private TMP_Text FireBallCountText;

    private void Awake()
    {
        UpdateText();
    }
    private void OnEnable()
    {
        //Check if the Input Actions are initialized before following the attack input
        if (InputManager.Actions != null)
        {
            InputManager.Actions.Game.ShootFireBall.performed += ShootFireBall;
        }
    }

    private void OnDisable()
    {
        //Check if the input actions exist before unfollowing the attack input
        if (InputManager.Actions != null)
        {
            InputManager.Actions.Game.ShootFireBall.performed -= ShootFireBall;
        }
    }

    //Called when player presses the attack input, spawns a fireball if the player has any fireball left
    private void ShootFireBall(InputAction.CallbackContext obj)
    {
        //Check if the player has any fireball left before trying to shoot
        if (FireBallCount <= 0)
        {
           Debug.Log("No more fireball to shoot");
            return;
        }

        //Check if the fireball prefab and attach point are assigned before trying to spawn a fireball
        if (FireBallPrefab != null && FireBallAttachPoint != null && FireBallCount > 0)
        {
            Instantiate(FireBallPrefab, FireBallAttachPoint.position, FireBallAttachPoint.rotation);

            FireBallCount--;
            UpdateText();
        }
    }

    //Adds fireball to the player
    public void AddFireBall(int amount = 5)
    {
        FireBallCount += amount;
        FireBallCount = Mathf.Max(FireBallCount, 0);
        UpdateText();
    }

    //Returns how many fireball the player currently have
    public int GetFireBallCount()
    {
        return FireBallCount;
    }
    //Updates the UI text to show current fireball count
    private void UpdateText()
    {
        if (FireBallCountText != null)
        {
            FireBallCountText.SetText($"FireballCount: {FireBallCount}");
        }
    }
}
