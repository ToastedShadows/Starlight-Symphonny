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

    public string TypeAndCategory
    {
        get
        {
            string typeText = Type.ToString();
            string categoryText = Category.ToString();
            
            // Convert the first letter of type and category to uppercase
            typeText = char.ToUpper(typeText[0]) + typeText.Substring(1);
            categoryText = char.ToUpper(categoryText[0]) + categoryText.Substring(1);
            
            // Combine type and category with " - "
            return $"{typeText} - {categoryText}";
        }
    }

    [System.Serializable]
    public class MoveEffects
    {
        [SerializeField] private List<StatsBoost> boosts;

        public List<StatsBoost> Boosts => boosts;
    }
}

[System.Serializable]
public class StatsBoost
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
