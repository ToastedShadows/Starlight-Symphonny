using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameState {FreeRoam, Dialog, Battle}

public class DialogController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    //serialzefield adds variable visible in unity inspector
    GameState state;

    private void Start()
    {
        DialogManager.Instance.OnShowDialog += () =>
        {
            state = GameState.Dialog;
        };
        
        DialogManager.Instance.OnHideDialog += () =>
        {
            if (state == GameState.Dialog)
                state = GameState.FreeRoam;
        };
    }

    public void Update()
    {
        if (state == GameState.FreeRoam)
        {
            playerController.HandleUpdate();
            
        } else if (state == GameState.Dialog)
        {
            DialogManager.Instance.HandleUpdate();
            
        } else if (state == GameState.Battle)
        {

        }
    }
}
