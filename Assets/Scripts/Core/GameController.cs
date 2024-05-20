using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerState { FreeRoam, Battle, Menu }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem;
    [SerializeField] Camera worldCamera;

    MenuController menuController;
    PlayerState state;

    private void Awake()
    {
        menuController = GetComponent<MenuController>();
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
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("Player closed the menu");
                menuController.CloseMenu();
                state = PlayerState.FreeRoam;
                playerController.SetCanMove(true);
            }
        }
    }
}
