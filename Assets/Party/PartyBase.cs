using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Team", menuName = "Team/Create new members")]
public class PartyMembers : ScriptableObject
{
    [SerializeField] string name;
    [SerializeField] string description;

    [SerializeField] Sprite frontSprite; // Assuming frontSprite and backSprite are Sprite type
    [SerializeField] Sprite backSprite;
    [SerializeField] ClassType classType1; // Corrected field names to match declaration
    [SerializeField] ClassType classType2;

    [SerializeField] int maxHp;
    [SerializeField] int maxMana;
    [SerializeField] int mAttack; // Changed to camelCase for consistency
    [SerializeField] int rAttack;
    [SerializeField] int mDefense; // Changed to camelCase for consistency
    [SerializeField] int rDefense;
    [SerializeField] int speed;
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