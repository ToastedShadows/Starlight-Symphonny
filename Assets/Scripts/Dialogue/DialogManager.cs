using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    [SerializeField] ChoiceBox choiceBox;
    [SerializeField] GameObject dialogBox;
    [SerializeField] Text dialogText;
    [SerializeField] int letterPerSecond;

    public event Action OnShowDialog;
    public event Action OnHideDialog;

    public static DialogManager Instance { get; private set; }

    private Dialog dialog;
    private int currentLine = 0;
    private bool isTyping;
    private bool dialogActive = false;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowDialog(Dialog dialog, List<string> choices = null, Action<int> onChoiceSelected = null)
    {
        if (!dialogActive)
        {
            OnShowDialog?.Invoke();
            this.dialog = dialog;
            dialogBox.SetActive(true);
            dialogActive = true;
            StartCoroutine(TypeDialog(dialog.Lines[0]));
        }

        if (choices != null && choices.Count > 1)
        {
            choiceBox.ShowChoices(choices, onChoiceSelected);
        }
    }

    public IEnumerator ShowDialogText(string text)
    {
        dialogBox.SetActive(true);
        dialogText.text = text;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        dialogBox.SetActive(false);
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Z) && !isTyping)
        {
            currentLine++;
            if (currentLine < dialog.Lines.Count)
            {
                StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else
            {
                dialogBox.SetActive(false);
                currentLine = 0;
                dialogActive = false;
                OnHideDialog?.Invoke();
            }
        }
    }

    private IEnumerator TypeDialog(string line)
    {
        isTyping = true;
        dialogText.text = "";
        foreach (var letter in line.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / letterPerSecond);
        }
        isTyping = false;
    }

    public bool IsDialogActive()
    {
        return dialogActive;
    }
}
