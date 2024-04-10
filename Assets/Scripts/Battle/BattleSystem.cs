using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

public class BattleSystem : MonoBehaviour
{
    [SerializeField] BattleUnit playerUnit;
    [SerializeField] BattleUnit enemyUnit;
    [SerializeField] BattleHud playerHud;
    [SerializeField] BattleHud enemyHud;
    [SerializeField] BattleDialogBox dialogBox;

    public event Action<bool> OnBattleOver;

    BattleState state;
    int currentAction;
    int currentMove; // Declare currentMove here

    public void StartBattle()
    {
        StartCoroutine(SetupBattle());
    }

    public IEnumerator SetupBattle()
    {
        playerUnit.Setup();
        enemyUnit.Setup();
        playerHud.SetData(playerUnit.Characters);
        enemyHud.SetData(enemyUnit.Characters);

        List<MoveBase> playerMoves = playerUnit.Characters.Moves.Select(move => move.Base).ToList();

        dialogBox.SetMoveNames(playerMoves);

        yield return StartCoroutine(dialogBox.TypeDialog($"A Wild {enemyUnit.Characters._base.CharacterName} Draws Near!"));
        yield return new WaitForSeconds(1f);

        PlayerAction();
    }

    void PlayerAction()
    {
        state = BattleState.PlayerAction;
        StartCoroutine(dialogBox.TypeDialog("Choose an Action"));
        dialogBox.EnableActionSelector(true);
    }

    void PlayerMove()
    {
        state = BattleState.PlayerMove;
        dialogBox.EnableActionSelector(false);
        dialogBox.EnableMoveSelector(true);
    }

    IEnumerator PerformPlayerMove()
    {
        state = BattleState.Busy;

        var move = playerUnit.Characters.Moves[currentMove];

        move.PP--;

        yield return dialogBox.TypeDialog($"{playerUnit.Characters._base.CharacterName} used {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        bool isFainted = enemyUnit.Characters.TakeDamage(move, playerUnit.Characters);

        yield return StartCoroutine(enemyHud.UpdateHP()); // Added StartCoroutine

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Characters._base.CharacterName} Fainted");

            yield return new WaitForSeconds(2f);
            OnBattleOver(true);
        }
        else
        {
            StartCoroutine(EnemyMove());
        }
    }

    IEnumerator EnemyMove()
    {
        state = BattleState.EnemyMove;

        var move = enemyUnit.Characters.GetRandomMove();

        move.PP--;

        yield return dialogBox.TypeDialog($"{enemyUnit.Characters._base.CharacterName} used {move.Base.Name}");

        yield return new WaitForSeconds(1f);

        bool isFainted = playerUnit.Characters.TakeDamage(move, enemyUnit.Characters);
        yield return StartCoroutine(playerHud.UpdateHP()); // Added StartCoroutine

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Characters._base.CharacterName} Fainted");
            yield return new WaitForSeconds(2f);
            OnBattleOver(false);
        }
        else
        {
            PlayerAction();
        }
    }

    public void HandleUpdate()
    {
        if (state == BattleState.PlayerAction)
        {
            HandleActionSelection();
        }
        else if (state == BattleState.PlayerMove)
        {
            HandleMoveSelection();
        }
    }

    void HandleActionSelection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentAction < 1)
                ++currentAction;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentAction > 0)
                --currentAction;
        }

        dialogBox.UpdateActionSelection(currentAction);

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentAction == 0)
            {
                PlayerMove();
            }
            else if (currentAction == 1)
            {
                // Run
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Characters.Moves.Count - 4)
                currentMove += 4;
            else
                currentMove = currentMove % 4; // Wrap around to the first row if at the last
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (currentMove >= 4)
                currentMove -= 4;
            else
            {
                int lastRowStart = playerUnit.Characters.Moves.Count - (playerUnit.Characters.Moves.Count % 4);
                if (lastRowStart == playerUnit.Characters.Moves.Count) // If the last row is full
                    lastRowStart -= 4;
                currentMove = Mathf.Min(lastRowStart + currentMove, playerUnit.Characters.Moves.Count - 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (currentMove < playerUnit.Characters.Moves.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (currentMove > 0)
                --currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.Z))
        {
            dialogBox.EnableMoveSelector(false);
            dialogBox.EnableDialogText(true);
            StartCoroutine(PerformPlayerMove());
        }

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Characters.Moves[currentMove]);
    }
}