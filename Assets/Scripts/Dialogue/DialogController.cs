using UnityEngine;

public class DialogController : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
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

        // Check for null references
        if (playerController == null) Debug.LogWarning("PlayerController is not assigned.");
    }

    public void Update()
    {
        if (state == GameState.FreeRoam)
        {
            if (playerController != null)
            {
                playerController.HandleUpdate();
            }
        }
        else if (state == GameState.Dialog)
        {
            if (DialogManager.Instance != null)
            {
                DialogManager.Instance.HandleUpdate();
            }
        }
        else if (state == GameState.Battle)
        {
            // Handle battle updates
        }
    }
}
