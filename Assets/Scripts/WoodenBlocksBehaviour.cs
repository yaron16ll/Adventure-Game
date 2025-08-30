using UnityEngine;

public class WoodenBlocksBehaviour : MonoBehaviour
{
    public Item Item;
    public InventoryManagerRiver inventoryManager;
    public AudioClip clickSound;
    
    public AudioSource folderAudioSource;
    private bool collected = false;
    public static bool isActive = true;

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && !collected && other.CompareTag("Player"))
        {
            bool added = inventoryManager.AddItem(Item);

            if (added)
            {
                collected = true;

                if (folderAudioSource != null && clickSound != null)
                    folderAudioSource.PlayOneShot(clickSound);

                Destroy(gameObject);
            }
            else
            {
                Collider col = GetComponent<Collider>();
                if (col != null)
                    col.enabled = false;

                Debug.Log("Inventory full for item: " + Item.type);
            }
        }
    }
}
