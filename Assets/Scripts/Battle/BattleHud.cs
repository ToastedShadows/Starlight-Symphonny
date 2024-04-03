using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    public void SetData(Characters characters)
    {
        nameText.text = characters._base.CharacterName;
        levelText.text = "Lvl " + characters.level; // Add a space between "Lvl" and the level value
        hpBar.SetHP((float)characters.HP / characters.MaxHp);
    }
}
