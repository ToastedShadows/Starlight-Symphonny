using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject menu; // Reference to the menu UI

    public void OpenMenu()
    {
        menu.SetActive(true);
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public bool IsMenuOpen()
    {
        return menu.activeSelf;
    }
}
