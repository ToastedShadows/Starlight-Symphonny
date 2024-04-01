using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Move", menuName = "Characters/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] string moveName; // Add SerializeField attribute
    [TextArea]
    [SerializeField] string description; // Add SerializeField attribute

    [SerializeField] MoveType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] int pp;

    public string Name
    {
        get { return moveName; } // Use moveName instead of name
    }
    public string Description
    {
        get { return description; }
    }
    public MoveType Type // Change return type to MoveType
    {
        get { return type; }
    }
    public int Power // Change return type to int
    {
        get { return power; }
    }
    public int Accuracy // Change return type to int
    {
        get { return accuracy; }
    }
    public int PP // Change return type to int
    {
        get { return pp; }
    }
}
