using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class ItemBase : ScriptableObject
{
    [SerializeField] protected string itemName;
    [SerializeField] protected string itemDescription;
    [SerializeField] protected Sprite itemIcon;

    public string Name = itemName;
    public string Description = itemDescription;
    public Sprite Icon = itemIcon;
}
