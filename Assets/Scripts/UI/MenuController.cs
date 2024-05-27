using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public List<Text> menuOptions;
    public Color highlightedColor = new Color(1, 1, 0, 1);
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
        if (menuOptions.Count == 0) return;

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
            menuOptions[i].color = (i == selectedOption) ? highlightedColor : normalColor;
        }
    }

    private void ExecuteSelectedOption()
    {
        switch (selectedOption)
        {
            case 0:
                Debug.Log("Inventory selected");
                break;
            case 1:
                Debug.Log("Save selected");
                break;
            case 2:
                Debug.Log("Party selected");
                break;
            case 3:
                Debug.Log("Main Menu selected");
                PlayerController playerController = FindObjectOfType<PlayerController>();
                if (playerController != null)
                {
                    playerController.StartSceneTransition();
                }
                SceneManager.LoadScene(0);
                break;
            case 4:
                Debug.Log("Quit selected");
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                break;
            default:
                Debug.LogWarning("Invalid menu option selected");
                break;
        }
    }
}
