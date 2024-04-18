using UnityEngine;

public enum PlayerState { FreeRoam, Battle }

public class GameController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] BattleSystem battleSystem; 
    [SerializeField] Camera worldCamera;

    PlayerState state;

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
        }
        else if (state == PlayerState.Battle)
        {
            battleSystem.HandleUpdate();
        }
    }
}
