using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogChoiceController : MonoBehaviour, Interactable
{
    [SerializeField] Dialog dialog;
    
    public void Interact()
    {
        Debug.Log("Interact method called");
        
        if (!DialogManager.Instance.IsDialogActive())
        {
            StartCoroutine(ShowDialogWithChoicesCoroutine());
        }
    }

    private IEnumerator ShowDialogWithChoicesCoroutine()
    {
        int selectedChoice = 0;

        Debug.Log("Attempting to show dialog choices");
        
        yield return DialogManager.Instance.ShowDialog(null, new List<string>() { "Yes", "No" },
            (choiceIndex) => selectedChoice = choiceIndex + 1);  // Adjusting index to start from 1

        Debug.Log($"Selected Choice: {selectedChoice}");

        yield return new WaitUntil(() => selectedChoice != 0);

        if (selectedChoice == 1)
        {
            DialogManager.Instance.ShowDialog(dialog);
        }
        else if (selectedChoice == 2)
        {
            // You can add another dialog or action here if needed
            yield return DialogManager.Instance.ShowDialogText("Okay! Come back if you change your mind");
        }
    }
}
