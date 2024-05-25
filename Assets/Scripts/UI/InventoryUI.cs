using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public void HandleUpdate(Action onBack)
    {
        if (input.GetKeyDown(KeyCode.X))
            onBack?.Invoke();
    }
}
