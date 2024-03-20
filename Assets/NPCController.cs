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
        DialogManager.Instance.ShowDialog(dialog);
        yield return null;
    }
}