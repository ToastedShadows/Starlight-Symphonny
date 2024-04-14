using System.Collections.Generic;
using UnityEngine;
using GameEnums;

public class Characters : MonoBehaviour
{
    public PartyBase _base { get; private set; }
    public int level { get; private set; }
    public MoveType moveType { get; private set; }
    public int HP { get; private set; }
    public List<Move> Moves { get; private set; }
    public Dictionary<Stat, int> Stats { get; private set; }
    public Dictionary<Stat, int> StatsBoosts { get; private set; }

    public void Initialize(PartyBase pBase, int pLevel, MoveType pMoveType)
    {
        _base = pBase;
        level = pLevel;
        moveType = pMoveType;
        Moves = new List<Move>();

        // Initialize Stats dictionary
        Stats = new Dictionary<Stat, int>
        {
            { Stat.MAttack, 0 },
            { Stat.RAttack, 0 },
            { Stat.MDefense, 0 },
            { Stat.RDefense, 0 },
            { Stat.MaxHp, 0 },
            { Stat.MaxMana, 0 }
        };

        StatsBoosts = new Dictionary<Stat, int>
        {
            { Stat.MAttack, 0 },
            { Stat.RAttack, 0 },
            { Stat.MDefense, 0 },
            { Stat.RDefense, 0 },
            { Stat.MaxHp, 0 },
            { Stat.MaxMana, 0 }
        };

        CalculateStats();

        HP = Stats[Stat.MaxHp];

        foreach (var move in _base.LearnableMoves)
        {
            if (move.Level <= level)
                Moves.Add(new Move(move.MoveBase, move.MoveBase.PP));

            if (Moves.Count >= 4)
                break;
        }
    }

    void CalculateStats()
    {
        Stats[Stat.MAttack] = Mathf.FloorToInt(_base.MAttack * level / 100f) + 5;
        Stats[Stat.RAttack] = Mathf.FloorToInt(_base.RAttack * level / 100f) + 5;
        Stats[Stat.MDefense] = Mathf.FloorToInt(_base.MDefense * level / 100f) + 5;
        Stats[Stat.RDefense] = Mathf.FloorToInt(_base.RDefense * level / 100f) + 5;
        Stats[Stat.MaxHp] = Mathf.FloorToInt(_base.MaxHp * level / 100f) + 5;
        Stats[Stat.MaxMana] = Mathf.FloorToInt(_base.MaxMana * level / 100f) + 5;
    }

    int GetStat(Stat stat)
    {
        int statVal = Stats[stat];

        int boost = StatsBoosts[stat];
        var boostValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

        if (boost >= 0)
            statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
        else
            statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);

        return statVal;
    }

    public void ApplyBoosts(List<StatsBoosts> statBoosts)
    {
        foreach (var statBoost in statBoosts)
        {
            var stat = statBoost.stat;
            var boost = statBoost.boost;

            StatsBoosts[stat] = Mathf.Clamp(StatsBoosts[stat] + boost, -6, 6);

            Debug.Log($"{stat} has been boosted to {StatsBoosts[stat]}");
        }
    }

    public int MAttack => GetStat(Stat.MAttack);
    public int RAttack => GetStat(Stat.RAttack);
    public int MaxHp => GetStat(Stat.MaxHp);
    public int MaxMana => GetStat(Stat.MaxMana);
    public int MDefense => GetStat(Stat.MDefense);
    public int RDefense => GetStat(Stat.RDefense);

    public bool TakeDamage(Move move, Characters attacker)
    {
        float attack = (move.Base.Category == MoveCategory.Ranged) ? attacker.RAttack : attacker.MAttack;
        float defense = (move.Base.Category == MoveCategory.Ranged) ? RDefense : MDefense;

        float modifiers = Random.Range(0.85f, 1f);
        float a = (2 * attacker.level + 10) / 250f;
        float d = a * move.Base.Power * ((float)attack / defense) + 2;
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

[System.Serializable]
public class StatsBoosts
{
    public Stat stat;
    public int boost;
}
