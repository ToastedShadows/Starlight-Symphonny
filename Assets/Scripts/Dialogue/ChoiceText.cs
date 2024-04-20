using UnityEngine;
using UnityEngine.UI;

public class ChoiceText : MonoBehaviour
{
    Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    public void SetSelected(bool selected)
    {
        if (text != null)
        {
            text.color = (selected) ? Color.yellow : Color.black;
        }
    }

    public Text TextField => text;
}
