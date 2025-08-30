using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item")]
public class Item : ScriptableObject
{
    public string name;
    public Sprite icon;
    public ItemType type;
}

public enum ItemType
{
    Empty, Mushroom, Garlic, Sage, WoodenBlock
}

