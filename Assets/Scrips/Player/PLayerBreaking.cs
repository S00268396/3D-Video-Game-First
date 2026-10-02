using UnityEngine;
using UnityEngine.InputSystem;

[DisallowMultipleComponent]
public class PLayerBreaking : MonoBehaviour
{
    //Breaking settings
    [Header("Breaking Settings")]
    [SerializeField] private float WeaponDistance = 3f;
    [SerializeField] private float WeaponDamage = 25f;
    [SerializeField] private float AttackCoolDown = 1f;
    [SerializeField] private LayerMask Interactables;
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private RaycastHit RayCastHit;

    private float LastAttackTime;

    private void OnEnable()
    {
        //Check if the Input Actions are initialized before following the attack input
        if (InputManager.Actions != null)
        {
            InputManager.Actions.Game.SwordAttack.performed += OnBreak;
        }
    }
    private void OnDisable()
    {
        //Check if the input actions exist before unfollowing the attack input
        if (InputManager.Actions != null)
        {
            InputManager.Actions.Game.SwordAttack.performed -= OnBreak;
        }
    }

    public void OnBreak(InputAction.CallbackContext obj)
    {

        //Ingore the attck input if the cooldown time has not yet passed since the last attck
        //Time.time returns the time in seconds since the last attack
        if (Time.time - LastAttackTime < AttackCoolDown)
        {
            return;
        }

        LastAttackTime = Time.time;

        Debug.Log("Break performed");
        CastRay();
    }
    private void CastRay()
    {
        if (Physics.Raycast(
            CameraTransform.position,
            CameraTransform.forward,
            out RayCastHit,
            WeaponDistance,
            Interactables))
        {
            if (RayCastHit.collider.gameObject.TryGetComponent<IBreakeable>(out IBreakeable Breaking))
            {
                if (Breaking.IsBreaking)
                {
                    Breaking.ApplyDamage(WeaponDamage);
                    Debug.Log($"{Breaking} hit for {WeaponDamage}");
                }
            }
        }

    }
}
