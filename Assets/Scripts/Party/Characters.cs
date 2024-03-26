using UnityEngine;

public class Characters : MonoBehaviour
{
    PartyBase _base;
    int level;

    public Characters(PartyBase pBase, int pLevel)
    {
        _base = pBase;
        level = pLevel;
    }

    public int MAttack => Mathf.FloorToInt(_base.MAttack * level / 100f) + 5;
    public int RAttack => Mathf.FloorToInt(_base.RAttack * level / 100f) + 5;
    public int MaxHp => Mathf.FloorToInt(_base.MaxHp * level / 100f) + 10;
    public int MaxMana => Mathf.FloorToInt(_base.MaxMana * level / 100f) + 10;
    public int MDefense => Mathf.FloorToInt(_base.MDefense * level / 100f) + 5; // Corrected property name
    public int RDefense => Mathf.FloorToInt(_base.RDefense * level / 100f) + 5; // Corrected property name
    public int Speed => Mathf.FloorToInt(_base.Speed * level / 100f) + 5;
}