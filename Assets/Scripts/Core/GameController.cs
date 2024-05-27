using UnityEngine;

public enum PlayerState { FreeRoam, Battle, Menu, Bag }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;
    [SerializeField] InventoryUI inventoryUI;

    MenuController menuController;
    PlayerState state;

    private void Awake()
    {
        menuController = GetComponent<MenuController>();

        // Check for null references
        if (playerController == null) Debug.LogWarning("PlayerController is not assigned.");
        if (battleSystem == null) Debug.LogWarning("BattleSystem is not assigned.");
        if (worldCamera == null) Debug.LogWarning("WorldCamera is not assigned.");
        if (inventoryUI == null) Debug.LogWarning("InventoryUI is not assigned.");
        if (menuController == null) Debug.LogWarning("MenuController is not assigned.");
    }

    private void Start()
    {
        playerController.OnEncountered += StartBattle;
        battleSystem.OnBattleOver += EndBattle;
    }

    void StartBattle()
    {
        state = PlayerState.Battle;
        battleSystem.gameObject.SetActive(true);
        worldCamera.gameObject.SetActive(false);

        battleSystem.StartBattle();
    }

    void OnMenuSelected(int selectedItem)
    {
        if (selectedItem == 0)
        {
            inventoryUI.gameObject.SetActive(true);
            state = PlayerState.Bag;
        }
    }

    void EndBattle(bool won)
    {
        state = PlayerState.FreeRoam;
        battleSystem.gameObject.SetActive(false);
        worldCamera.gameObject.SetActive(true);
        playerController.HandleUpdate(); // Allow player movement after battle ends
    }

    public void Update()
    {
        if (state == PlayerState.FreeRoam)
        {
            playerController.HandleUpdate();
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Player pressed Enter key");
                menuController.OpenMenu();
                state = PlayerState.Menu;
                playerController.SetCanMove(false);
            }
        }
        else if (state == PlayerState.Battle)
        {
            battleSystem.HandleUpdate();
        }
        else if (state == PlayerState.Menu)
        {
            menuController.HandleUpdate();
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Player closed the menu");
                menuController.CloseMenu();
                state = PlayerState.FreeRoam;
                playerController.SetCanMove(true);
            }
        }
        else if (state == PlayerState.Bag)
        {
            if (Input.GetKeyDown(KeyCode.Z))
            {
                inventoryUI.gameObject.SetActive(false);
                state = PlayerState.FreeRoam;
            }
        }
    }
}
