using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Team", menuName = "Characters/Create new members")]
public class PartyBase : ScriptableObject
{
    [SerializeField] private string characterName; // Renamed to avoid hiding the inherited member

    [SerializeField] private string description;

    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private ClassType classType1;
    [SerializeField] private ClassType classType2;

    [SerializeField] private int maxHp;
    [SerializeField] private int maxMana;
    [SerializeField] private int mAttack;
    [SerializeField] private int rAttack;
    [SerializeField] private int mDefense;
    [SerializeField] private int rDefense;
    [SerializeField] private int speed;

    [SerializeField] private List<LearnableMove> learnableMoves;

    public string CharacterName => characterName; // Renamed property to avoid hiding the inherited member
    public string Description => description;
    public Sprite FrontSprite => frontSprite;
    public Sprite BackSprite => backSprite;
    public ClassType ClassType1 => classType1;
    public ClassType ClassType2 => classType2;
    public int MaxHp => maxHp;
    public int MaxMana => maxMana;
    public int MAttack => mAttack;
    public int RAttack => rAttack;
    public int MDefense => mDefense;
    public int RDefense => rDefense;
    public int Speed => speed;
    public List<LearnableMove> LearnableMoves => learnableMoves; // Renamed property to avoid naming conflict
}

[System.Serializable]
public class LearnableMove
{
    [SerializeField] private MoveBase moveBase;
    [SerializeField] private int level;

    public MoveBase MoveBase => moveBase; // Renamed property to avoid naming conflict
    public int Level => level;
}

public enum ClassType
{
    None,
    Mage,
    Brute,
    Healer,
    Assassin,
    Fighter,
    Ninja
}
