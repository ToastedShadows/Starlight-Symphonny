using UnityEngine;
using System.Collections.Generic;

public class Cutscene : MonoBehaviour
{
    [SerializeField] 
    private List<CutsceneAction> actions = new List<CutsceneAction>();

    public void AddAction(CutsceneAction action)
    {
        action.Name = action.GetType().ToString();
        if (action is DialogueAction dialogueAction)
        {
            dialogueAction.Dialog.Lines.Add("Default Dialog Line"); // Add a default line
        }
        actions.Add(action);
    }
}
