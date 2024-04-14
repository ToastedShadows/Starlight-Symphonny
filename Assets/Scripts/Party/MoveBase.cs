using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Move", menuName = "Characters/Create new move")]
public class MoveBase : ScriptableObject
{
    [SerializeField] private string moveName;
    [TextArea]
    [SerializeField] private string description;

    [SerializeField] private MoveType type;
    [SerializeField] private int power;
    [SerializeField] private int accuracy;
    [SerializeField] private int pp;
    [SerializeField] private MoveCategory category;
    [SerializeField] private MoveEffects effects;
    [SerializeField] private MoveTarget target;

    public string Name => moveName;
    public string Description => description;
    public MoveType Type => type;
    public int Power => power;
    public int Accuracy => accuracy;
    public int PP => pp;
    public MoveCategory Category => category;
    public MoveEffects Effects => effects;
    public MoveTarget Target => target;

    [System.Serializable]
    public class MoveEffects
    {
        [SerializeField] private List<StatsBoosts> boosts;

        public List<StatsBoosts> Boosts => boosts;
    }
}

[System.Serializable]
public class StatsBoosts
{
    public Stat stat;
    public int boost;
}

public enum MoveType
{
    Melee,
    Ranged
}

public enum MoveCategory
{
    Melee, 
    Ranged, 
    Status
}

public enum MoveTarget
{
    Foe, 
    Self
}
