using TMPro;
using UnityEngine;

public class PlayerPickUp : MonoBehaviour, IInteractable
{
    [Header("Pick Up Settings: ")]
    [SerializeField]
    private Item item; //The item this pickup
    [SerializeField]
    private float pickUpRange = 2f; //How close the player must be to interact
    [SerializeField]
    private LayerMask pickUpLayer; //Layer filter for pickup interaction
    [SerializeField]
    private float distance; //Distance between the player and the pickup

    //New item to be set when the player picks up a new item
    public void SetItems(Item NewItem)
    {
        item = NewItem;
    }

    //Checks if the player is close enough to interact with the pickup
    public bool CanInteractWith(GameObject Interactor)
    {
        distance = Vector3.Distance(transform.position, Interactor.transform.position);
        return distance <= pickUpRange;
    }

    //When the player interacts with the pickup, use the item and destroy the pickup object.
    public void Interact(GameObject Interactor)
    {
        item.Use(Interactor);

        Destroy(gameObject);
    }
}
