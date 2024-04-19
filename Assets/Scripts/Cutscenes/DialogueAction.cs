using UnityEngine;

[System.Serializable]
public class DialogueAction : CutsceneAction
{
    [SerializeField]
    private Dialog dialog;

    public Dialog Dialog
    {
        get { return dialog; }
        set { dialog = value; }
    }
}
