using UnityEngine;
using System.Collections.Generic;

public class Characters : MonoBehaviour
{
    public PartyBase _base { get; set; }
    public int level { get; set; }
    public MoveType moveType { get; set; } // Added MoveType property
    public int HP { get; set; }
    public List<Move> Moves { get; set; }

    // Constructor without circular instantiation
    public Characters(PartyBase pBase, int pLevel, MoveType pMoveType)
    {
        _base = pBase;
        level = pLevel;
        HP = MaxHp;
        moveType = pMoveType;
        Moves = new List<Move>();

        foreach (var move in _base.LearnableMoves)
        {
            if (move.Level <= level)
                Moves.Add(new Move(move.MoveBase, move.MoveBase.PP)); // Fixed constructor

            if (Moves.Count >= 4)
                break;
        }
    }

    public int MAttack => Mathf.FloorToInt(_base.MAttack * level / 100f) + 5;
    public int RAttack => Mathf.FloorToInt(_base.RAttack * level / 100f) + 5;
    public int MaxHp => Mathf.FloorToInt(_base.MaxHp * level / 100f) + 10;
    public int MaxMana => Mathf.FloorToInt(_base.MaxMana * level / 100f) + 10;
    public int MDefense => Mathf.FloorToInt(_base.MDefense * level / 100f) + 5;
    public int RDefense => Mathf.FloorToInt(_base.RDefense * level / 100f) + 5;
    public int Speed => Mathf.FloorToInt(_base.Speed * level / 100f) + 5;

    public bool TakeDamage(Move move, Characters attacker)
    {
        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attacker.MAttack / MDefense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        HP -= damage;
        if (HP <= 0)
        {
            HP = 0;
            return true;
        }

        return false;
    }

    public Move GetRandomMove()
    {
        int r = Random.Range(0, Moves.Count);
        return Moves[r];
    }
}
