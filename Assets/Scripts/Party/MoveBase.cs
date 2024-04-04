using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Move", menuName = "Characters/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string moveName; // Add SerializeField attribute
    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private MoveType type;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;

    public string Name => moveName; // Add Name property to access move name
    public string Description => description;
    public MoveType Type => type;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;
}

public enum MoveType
{
    Melee,
    Ranged
}
