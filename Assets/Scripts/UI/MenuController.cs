using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu; // Reference to the menu UI
    public List<Text> menuOptions;
    public Color highlightedColor = new Color(1, 1, 0, 1); // Ensure alpha is set to 1 (full opacity)
    public Color normalColor = Color.black;

    private int selectedOption = 0;

    private void Start()
    {
        if (menuOptions.Count > 0)
        {
            UpdateMenuSelection();
        }
        else
        {
            Debug.LogWarning("Menu options list is empty!");
        }
    }

    public void OpenMenu()
    {
        menu.SetActive(true);
        selectedOption = 0;
        UpdateMenuSelection();
    }

    public void CloseMenu()
    {
        menu.SetActive(false);
    }

    public bool IsMenuOpen()
    {
        return menu.activeSelf;
    }

    public void HandleUpdate()
    {
        if (menuOptions.Count == 0) return; // Ensure we have menu options to navigate

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedOption = (selectedOption + 1) % menuOptions.Count;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedOption = (selectedOption - 1 + menuOptions.Count) % menuOptions.Count;
            UpdateMenuSelection();
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            ExecuteSelectedOption();
        }
    }

    private void UpdateMenuSelection()
    {
        for (int i = 0; i < menuOptions.Count; i++)
        {
            if (i == selectedOption)
            {
                menuOptions[i].color = highlightedColor;
                Debug.Log($"Setting option {i} to highlighted color {highlightedColor}");
            }
            else
            {
                menuOptions[i].color = normalColor;
                Debug.Log($"Setting option {i} to normal color {normalColor}");
            }
        }
    }

    private void ExecuteSelectedOption()
    {
        // Handle the logic for each menu option
        switch (selectedOption)
        {
            case 0:
                Debug.Log("Inventory selected");
                // Open inventory logic here
                break;
            case 1:
                Debug.Log("Save selected");
                // Save game logic here
                break;
            case 2:
                Debug.Log("Load selected");
                // Load game logic here
                break;
            default:
                Debug.LogWarning("Invalid menu option selected");
                break;
        }
    }
}
