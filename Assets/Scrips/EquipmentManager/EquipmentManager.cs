using System.Collections.Generic;
using UnityEngine;
using static Item;

public class EquipmentManager : MonoBehaviour
{
    public WeaponItem EquippedWeapon {  get; private set; }
    public PotionItem EquippedPotion { get; private set; }

    [SerializeField] 
    private Transform WeaponAttachPoint;
    [SerializeField]
    private GameObject EquippedWeaponInstance;

    [SerializeField]
    private Transform PotionAttachPoint;
    [SerializeField]
    private GameObject EquippedPotionInstance;

    public event ItemHandler OnEquipped;
    public event ItemHandler OnUnEquipped;

    public void EquipItem(Item item)
    {
        switch (item.Type)
        {
            case ItemType.Weapon:
                if (EquippedWeapon != (WeaponItem)item)
                {
                    OnUnEquipped?.Invoke(EquippedWeapon);
                    EquippedWeapon = (WeaponItem)item;
                    if (EquippedWeaponInstance != null)
                    {
                        Destroy(EquippedWeaponInstance);
                    }
                    EquippedWeaponInstance = Instantiate(item.Prefab, WeaponAttachPoint);
                    OnEquipped?.Invoke(EquippedWeapon);
                }
                break;
            case ItemType.Potion:
                if (EquippedWeapon != (PotionItem)item)
                {
                    OnUnEquipped?.Invoke(EquippedPotion);
                    EquippedPotion = (PotionItem)item;
                    if (EquippedPotionInstance != null)
                    {
                        Destroy(EquippedPotionInstance);
                    }
                    EquippedPotionInstance = Instantiate(item.Prefab, PotionAttachPoint);
                    OnEquipped?.Invoke(EquippedPotion);
                }
                break;
        }
    }
}
