using UnityEngine;

public class LuckyLootBlock : MonoBehaviour, IFocusable, IBreakeable
{
    //Color settings for the block when it is focused and not focused
    public Color DefaultColor = Color.yellow;
    public Color FocusedColor = Color.green;
    private MeshRenderer _Renderer;
    private Material _Material;

    //Loot Item and spawn point settings for the block
    public Item LootItem;
    [SerializeField]
    public Transform SpawnPoint;
    [SerializeField]
    public LayerMask Interactables;

    private void Awake()
    {
        //Get the MeshRenderer attached to the object
        _Renderer = GetComponent<MeshRenderer>();

        //If the MeshRenderer exists, get the material from it
        if (_Renderer != null)
        {
            _Material = _Renderer.material;
        }

        //Default the spawn point to the object's position if none assigned
        if (SpawnPoint == null)
        {
            SpawnPoint = transform;
        }
    }

    //When the object is focused, change its color to the focused color.
    public void OnFocus(GameObject Interactor)
    {
        if (_Material != null)
        { 
            _Material.color = FocusedColor;
        }
    }
    //When the object is unfocused, change its color to the unfocused color.
    public void UnFocus(GameObject Interactor)
    {
        if (_Material != null)
        {
            _Material.color = DefaultColor;
        }
    }

    //The block can be broken, so this property returns true.
    public bool IsBreaking
    {
        get { return true; }
    }

    //When damage is applied to the block, spawn the loot item and destroy the block.
    public void ApplyDamage(float DamageAmount)
    {

        // Spawn loot item
        if (LootItem != null && LootItem.Prefab != null)
        {
            //Instantiate the loot item at the spawn point with the same rotation as the block
            GameObject ItemInstantiate =  Instantiate(LootItem.Prefab, SpawnPoint.position, SpawnPoint.rotation);

            //Get the PlayerPickUp component from the instantiated item
            PlayerPickUp playerPickUp = ItemInstantiate.GetComponent<PlayerPickUp>();

            // if it doesn't exist, add it to the item
            if (playerPickUp == null)
            {
                playerPickUp = ItemInstantiate.AddComponent<PlayerPickUp>();    
            }

            playerPickUp.SetItems(LootItem);


        }
        // Destroy the block
        Destroy(gameObject);

    }

    
}
