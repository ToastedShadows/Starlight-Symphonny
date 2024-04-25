using System.Collections;
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
        yield return DialogManager.Instance.ShowDialog(dialog);
    }
}
