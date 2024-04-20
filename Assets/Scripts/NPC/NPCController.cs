using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;

    public void Interact()
    {
        // Check if the dialog manager is not showing a dialog already
        if (!DialogManager.Instance.IsDialogActive())
        {
            StartCoroutine(ShowDialogCoroutine());
        }
    }

    private IEnumerator ShowDialogCoroutine()
    {
        int selectedChoice = 0;

        DialogManager.Instance.ShowDialog(dialog, new List<string>() { "Yes", "No" },
            (choiceIndex) => selectedChoice = choiceIndex);

        yield return new WaitUntil(() => selectedChoice != 0);

        if (selectedChoice == 1)
        {
            // Yes
            yield return DialogManager.Instance.ShowDialogText("Okay");
        }
        else if (selectedChoice == 2)
        {
            // No
            yield return DialogManager.Instance.ShowDialogText("Okay! Come back if you change your mind");
        }
    }
}
