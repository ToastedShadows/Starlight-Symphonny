using UnityEngine;

public class ItemBase : ScriptableObject
{
    [Header("Key Information")]
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField] protected Sprite itemIcon;

    public string Name => itemName;
    public string Description => itemDescription;
    public Sprite Icon => itemIcon;
}

