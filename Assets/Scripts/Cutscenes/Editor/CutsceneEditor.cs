using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Cutscene))]
public class CutsceneEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var cutscene = target as Cutscene;

        if (GUILayout.Button("Add Dialogue Action"))
        {
            var dialogueAction = new DialogueAction();
            dialogueAction.Dialog = new Dialog();
            dialogueAction.Dialog.Lines = new List<string>(); // Initialize lines list
            cutscene.AddAction(dialogueAction);
        }
        else if (GUILayout.Button("Add Move Actor Action"))
        {
            cutscene.AddAction(new MoveActorAction());
        }

        base.OnInspectorGUI();
    }
}
