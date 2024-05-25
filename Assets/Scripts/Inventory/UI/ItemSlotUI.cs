using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;  // Import the TextMeshPro namespace

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] TMP_Text nameText;  // Change Text to TMP_Text
    [SerializeField] TMP_Text countText;  // Change Text to TMP_Text

    public void SetData(ItemSlot itemSlot)
    {
        nameText.text = itemSlot.Item.Name;
        countText.text = $"X {itemSlot.Count}";
    }
}
