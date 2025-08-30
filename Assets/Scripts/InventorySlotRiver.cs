using UnityEngine;
using UnityEngine.UI;

public class InventorySlotRiver : MonoBehaviour
{
    public Image icon;               // The icon that represents the item
    public Text numItemsText;       // The text that shows how many items in this slot

    ItemType type = ItemType.Empty;
    int numItems = 0;
    bool isSelected = false;

    void Start()
    {
        // Debug to confirm initialization
        Debug.Log("InventorySlot initialized: " + gameObject.name);
    }

    public bool AddItem(Item item)
    {
        if (type == ItemType.Empty || item.type == type)
        {
            
                icon.sprite = item.icon;
                type = item.type;
                numItems++;
                numItemsText.text = numItems.ToString();
                return true;
           
        }
        return false;
    }

    public bool RemoveItem(ItemType t)
    {
        if (t == type)
        {
            if (numItems > 1)
            {
                numItems--;
                numItemsText.text = numItems.ToString();
            }
            else
            {
                numItems = 0;
                numItemsText.text = "Empty";
                icon.sprite = null;
                type = ItemType.Empty;
            }
            return true;
        }
        return false;
    }

    public bool IsOfTheSameType(ItemType anotherType)
    {
        return anotherType == type;
    }

    public void Select()
    {
        isSelected = true;
        Debug.Log("Slot selected: " + gameObject.name);
    }

    public void DeSelect()
    {
        isSelected = false;
    }
    public int GetItemCount()
    {
        return numItems;
    }

    public bool getIsSelected()
    {
        return isSelected;
    }

    public ItemType GetItemType()
    {
        return type;
    }

    // Connect this function to the button's OnClick in the Unity Editor
    public void OnClick()
    {
        FindObjectOfType<InventoryManagerRiver>().SelectSlot(this);
    }
}
