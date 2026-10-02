using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerInteraction : MonoBehaviour
{
    public Transform CameraTransForm;
    public float InteractionDistance = 3f;
    public LayerMask InteractionLayer;
    private RaycastHit RayCastHit;
    private IInteractable CurrentInteractable;
    private IFocusable CurrentFocusable;

    private void OnEnable()
    {
        InputManager.Actions.Game.Interact.performed += OnInteract;
    }
    private void OnDisable()
    {
        InputManager.Actions.Game.Interact.performed -= OnInteract;
        ClearFocus();
    }
    public void OnInteract(InputAction.CallbackContext obj)
    {
        if (CurrentInteractable != null)
        {
            if (CurrentInteractable.CanInteractWith(gameObject))
            {
                CurrentInteractable.Interact(gameObject);   
            }
        }
    }
    private void Update()
    {
        CastRay();
    }
    private void CastRay()
    {
        if (Physics.Raycast(
            CameraTransForm.position,
            CameraTransForm.forward,
            out RayCastHit,
            InteractionDistance,
            InteractionLayer))
        {
            RayCastHit.collider.gameObject.TryGetComponent<IInteractable>(out CurrentInteractable);
            RayCastHit.collider.gameObject.TryGetComponent<IFocusable>(out IFocusable NewFocusable);

            if (NewFocusable != CurrentFocusable)
            {
                ClearFocus();
                CurrentFocusable = NewFocusable;

                if (CurrentFocusable != null)
                {
                    CurrentFocusable.OnFocus(gameObject);
                }
            }
        }
        else
        {
            ClearFocus();
        }
    }
    private void ClearFocus()
    {
        if (CurrentFocusable != null && (CurrentFocusable as object) != null)
        {
            CurrentFocusable.UnFocus(gameObject);
        }

        CurrentFocusable = null;
        CurrentInteractable = null;
    }
}
