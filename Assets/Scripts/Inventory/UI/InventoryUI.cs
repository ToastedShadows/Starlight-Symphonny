using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] GameObject itemList;
    [SerializeField] ItemSlotUI itemSlotUI;

    Inventory inventory;

    private void Awake()
    {
        inventory = Inventory.GetInventory();
    }

    private void Start()
    {
        UpdateItemList();
    }

    void UpdateItemList()
    {
        foreach (Transform child in itemList.transform)
            Destroy(child.gameObject);

        foreach (var itemSlot in inventory.Slots)
        {
            var slotUIObj = Instantiate (itemSlotUI, itemList.transform);
            slotUIObj.SetData(itemSlot);
        }
    }

    public void HandleUpdate(Action onBack)
    {
        if (Input.GetKeyDown(KeyCode.Z))
            onBack?.Invoke();
    }
}
