using UnityEngine;

public interface IInteractable
{
    public bool CanInteractWith(GameObject Interactor);
    public void Interact(GameObject Interactor);
}
