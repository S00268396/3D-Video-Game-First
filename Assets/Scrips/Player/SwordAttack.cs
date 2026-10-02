using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class SwordAttack : MonoBehaviour
{
    //Maybe keep this like this for now
    //Attack settings for the weapon, such as distance, damage, and cooldown time.
    [Header("Attack Settings")]
    [SerializeField] private float WeaponDistance = 3f;
    [SerializeField] private float WeaponDamage = 1f;
    [SerializeField] private float AttackCoolDown = 1f;
    [SerializeField] private LayerMask EnemyLayer;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private RaycastHit RayCastHit;

    //Tracks the time of the last attack for cooldown purposes.
    private float LastAttackTime;
    private void OnEnable()
    {
        //Check if the Input Actions are initialized before following the attack input
        if (InputManager.Actions != null)
        {
            InputManager.Actions.Game.SwordAttack.performed += OnAttack;
        }
    }

    private void OnDisable()
    {
        //Check if the input actions exist before unfollowing the attack input
        if (InputManager.Actions != null)
        {
            InputManager.Actions.Game.SwordAttack.performed -= OnAttack;
        }
    }

    public void OnAttack(InputAction.CallbackContext obj)
    {
        //Ingore the attck input if the cooldown time has not yet passed since the last attck
        //Time.time returns the time in seconds since the last attack
        if (Time.time - LastAttackTime < AttackCoolDown)
        {
            return;
        }

        //Upate the time of the last attack to the current time
        LastAttackTime = Time.time;

        //Perform the attack
        CastRay();
        Debug.Log("Sword Attack performed");


    }
    public void CastRay()
    {
        if (Physics.Raycast(
            CameraTransform.position,
            CameraTransform.forward,
            out RayCastHit,
            WeaponDistance,
            EnemyLayer))
        {
            //Check if the object hit implements IBreakeable
            if (RayCastHit.collider.gameObject.TryGetComponent<IDamageable>(out IDamageable damageable))
            {
                //Apply damage to the object if the object can currently be broken
                if (damageable.IsAlive)
                {
                    damageable.ApplyDamage(WeaponDamage);
                    Debug.Log($"{damageable} hit for {WeaponDamage}");
                }
            }
        }
    }
}
