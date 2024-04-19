using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
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

    public void ShowDialog(Dialog dialog)
    {
        if (!dialogActive)
        {
            OnShowDialog?.Invoke();
            this.dialog = dialog;
            dialogBox.SetActive(true);
            dialogActive = true;
            StartCoroutine(TypeDialog(dialog.Lines[0]));
        }
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
