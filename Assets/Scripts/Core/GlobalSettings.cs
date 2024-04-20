using UnityEngine;

public class GlobalSettings : MonoBehaviour
{
    public static GlobalSettings i;

    public Color HighlightedColor = Color.yellow;

    private void Awake()
    {
        if (i == null)
        {
            i = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
