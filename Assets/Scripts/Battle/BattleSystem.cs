using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using GameEnums;

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
    int currentMove;

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

        List<string> playerMoves = playerUnit.Characters.Moves.Select(move => move.Base.Name).ToList();

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

        yield return StartCoroutine(enemyHud.UpdateHP());

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{enemyUnit.Characters._base.CharacterName} Fainted");
            yield return new WaitForSeconds(2f);
            OnBattleOver?.Invoke(true);
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
        yield return StartCoroutine(playerHud.UpdateHP());

        if (isFainted)
        {
            yield return dialogBox.TypeDialog($"{playerUnit.Characters._base.CharacterName} Fainted");
            yield return new WaitForSeconds(2f);
            OnBattleOver?.Invoke(false);
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
            currentAction = (currentAction + 1) % 4; // Assuming you have 4 actions (Fight, Act, Item, Run)
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentAction = (currentAction + 3) % 4; // Assuming you have 4 actions (Fight, Act, Item, Run)
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
                // Handle "Act" action
            }
            else if (currentAction == 2)
            {
                // Handle "Item" action
            }
            else if (currentAction == 3)
            {
                // Handle "Run" action
            }
        }
    }

    void HandleMoveSelection()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (currentMove < playerUnit.Characters.Moves.Count - 1)
                ++currentMove;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
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

        dialogBox.UpdateMoveSelection(currentMove, playerUnit.Characters.Moves[currentMove].Base.Name);
    }
}
