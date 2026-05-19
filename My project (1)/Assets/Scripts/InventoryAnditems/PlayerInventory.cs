using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private List<InventoryItem> items = new List<InventoryItem>();

    private InventoryItem equippedItem;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            EquipItem(0);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EquipItem(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            EquipItem(2);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipItem(3);
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            EquipItem(4);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            EquipItem(5);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            EquipItem(6);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            EquipItem(7);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            EquipItem(8);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            UseEquippedItem();
        }
    }

    public void AddItem(InventoryItem item)
    {
        items.Add(item);

        item.gameObject.SetActive(false);
        item.transform.SetParent(transform);

        Debug.Log(item.itemName + " »πµÊ");
    }

    private void EquipItem(int index)
    {
        if (index < 0 || index >= items.Count)
        {
            return;
        }

        if (equippedItem != null)
        {
            equippedItem.gameObject.SetActive(false);
        }

        equippedItem = items[index];

        equippedItem.gameObject.SetActive(true);
        equippedItem.transform.SetParent(transform);
        equippedItem.transform.localPosition = new Vector3(0.45f, -0.1f, 0f);
        equippedItem.transform.localRotation = Quaternion.identity;

        Debug.Log((index + 1) + "æ∆¿Ã≈€ ¿Â¬¯ " + equippedItem.itemName);
    }

    private void UseEquippedItem()
    {
        if (equippedItem == null)
        {
            return;
        }

        equippedItem.Use(gameObject);

        items.Remove(equippedItem);
        Destroy(equippedItem.gameObject);

        equippedItem = null;
    }
}