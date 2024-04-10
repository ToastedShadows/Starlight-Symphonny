using UnityEngine;
using UnityEngine.UI;

public class BattleUnit : MonoBehaviour
{
    [SerializeField] PartyBase _base;
    [SerializeField] int level;
    [SerializeField] bool isPlayerUnit;

    public Characters Characters { get; private set; }

    public void Setup()
    {
        Characters = gameObject.AddComponent<Characters>(); // Add Characters component to the GameObject
        Characters.Initialize(_base, level, MoveType.Melee); // Initialize the Characters instance
        if (isPlayerUnit)
            GetComponent<Image>().sprite = _base.BackSprite; // Set front sprite for player unit
        else
            GetComponent<Image>().sprite = _base.FrontSprite; // Set back sprite for enemy unit
    }
}