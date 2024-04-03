using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PartyBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Characters Characters { get; set; }

    public void Setup()
    {
        Characters = new Characters(_base, level, MoveType.Melee); // Use an existing MoveType value
        if (isPlayerUnit)
            GetComponent<Image>().sprite = Characters._base.BackSprite; // Fixed: Set front sprite for player unit
        else
            GetComponent<Image>().sprite = Characters._base.FrontSprite; // Fixed: Set back sprite for enemy unit
    }
}
