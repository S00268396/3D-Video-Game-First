using System;
using Unity.VisualScripting;
using UnityEngine;
[Serializable]
public enum ItemType {Weapon, Potion, other}

[CreateAssetMenu(menuName = "Items/Item")]
public class Item : ScriptableObject
{
    [field: SerializeField]
    public string ID { get; private set; }
    public string Name { get; private set; }
    [TextArea(3, 10)]public string Desription { get; private set; }

    [field: SerializeField]
    public ItemType Type { get; private set; } = ItemType.other;
    [field: SerializeField]
    public Sprite Icon { get; private set; }
    [field: SerializeField]
    public GameObject Prefab { get; private set; }

    public delegate void ItemHandler(Item item);

    public virtual void Use(GameObject User) { }

    private void OnValidate()
    {
        if (string.IsNullOrEmpty(ID))
        {
            ID = System.Guid.NewGuid().ToString();
            UnityEditor.EditorUtility.SetDirty(this);
        }
    }
}
