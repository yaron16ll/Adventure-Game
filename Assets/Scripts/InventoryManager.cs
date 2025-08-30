using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventorySlot[] inventorySlots;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool HasAtLeastTwoOfEach(ItemType[] requiredTypes)
    {
        Dictionary<ItemType, int> itemCounts = new Dictionary<ItemType, int>();

        foreach (ItemType type in requiredTypes)
            itemCounts[type] = 0;

        foreach (InventorySlot slot in inventorySlots)
        {
            ItemType type = slot.GetItemType();
            if (itemCounts.ContainsKey(type))
            {
                itemCounts[type] += slot.GetItemCount(); 
            }
        }

        foreach (ItemType type in requiredTypes)
        {
            if (itemCounts[type] < 3)
                return false;
        }

        return true;
    }

    public bool RemoveItems(ItemType type, int amountToRemove)
    {
        foreach (InventorySlot slot in inventorySlots)
        {
            if (slot.GetItemType() == type)
            {
                while (amountToRemove > 0 && slot.GetItemCount() > 0)
                {
                    slot.RemoveItem(type);
                    amountToRemove--;
                }

                if (amountToRemove == 0)
                    return true; 
            }
        }
        return false; 
    }


    public bool AddItem(Item item)
    {
        InventorySlot tmpSlot = FindSlot(item);
        if (tmpSlot != null)
        {
            return tmpSlot.AddItem(item); 
        }
        return false;
    }


    public bool RemoveSelectedItem(Item item)
    {
        int index = GetSelectedSlot();
        if (index != -1)
        {
            return inventorySlots[index].RemoveItem(item.type);

        }
        return false;
    }

    // Select a slot while deselecting other slots
    public void SelectSlot(InventorySlot s)
    {
        int index;

        for (index = 0; index < inventorySlots.Length; index++)
        {
            if (inventorySlots[index] == s)
                inventorySlots[index].Select();
            else inventorySlots[index].DeSelect();
        }
    }
    public int GetSelectedSlot()
    {
        int index;

        for (index = 0; index < inventorySlots.Length; index++)
            if (inventorySlots[index].getIsSelected())
                return index;
        return -1;
    }


    InventorySlot FindSlot(Item item)
    {
        int i;
        for (i = 0; i < inventorySlots.Length; i++)
        {
            if (inventorySlots[i].IsOfTheSameType(item.type) ||
                inventorySlots[i].IsOfTheSameType(ItemType.Empty))
                return inventorySlots[i];
        }
        return null;
    }
}
