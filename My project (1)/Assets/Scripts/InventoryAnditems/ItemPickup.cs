using UnityEngine;

public class ItemPickup : MonoBehaviour
{


    private bool picked = false;
   private void OnTriggerEnter2D(Collider2D other)
    {

        if (picked == true)
        {
            return;
        }

        PlayerInventory inventory = other.GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            inventory = other.GetComponentInParent<PlayerInventory>();
        }

        if (inventory == null)
        {
            return;
        }

        InventoryItem item = GetComponent<InventoryItem>();

        if (item == null)
        {
            return;
        }

        picked = true;

        Collider2D col = GetComponent<Collider2D>();

        if (col != null)
        {
            col.enabled = false;
        }

        inventory.AddItem(item);
    }
}