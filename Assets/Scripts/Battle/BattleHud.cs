using UnityEngine;
using UnityEngine.UI;

public class BattleHud : MonoBehaviour
{
    [SerializeField] Text nameText;
    [SerializeField] Text levelText;
    [SerializeField] HPBar hpBar;

    Characters _characters; // Changed variable name to match private field

    public void SetData(Characters characters)
    {
        _characters = characters;
        nameText.text = characters._base.CharacterName;
        levelText.text = "Lvl " + characters.level; // Add a space between "Lvl" and the level value
        hpBar.SetHP((float)characters.HP / characters.MaxHp);
    }

    public void UpdateHP()
    {
        hpBar.SetHP((float)_characters.HP / _characters.MaxHp); // Fixed reference to use _characters field
    }
}
