using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameDialogueState
{
    FreeRoam,
    Dialog,
    Battle
}

public class DialogManager : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    GameDialogueState state;

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
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        // Check for null references
        if (playerController == null) Debug.LogWarning("PlayerController is not assigned.");
        if (choiceBox == null) Debug.LogWarning("ChoiceBox is not assigned.");
        if (dialogBox == null) Debug.LogWarning("DialogBox is not assigned.");
        if (dialogText == null) Debug.LogWarning("DialogText is not assigned.");
    }

    public void Update()
    {
        if (state == GameDialogueState.FreeRoam)
        {
            if (playerController != null)
            {
                playerController.HandleUpdate();
            }
        }
        else if (state == GameDialogueState.Dialog)
        {
            HandleUpdate();
        }
        else if (state == GameDialogueState.Battle)
        {
            // Handle battle updates
        }
    }

    public void HandleUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Z) && !isTyping)
        {
            if (dialog == null || dialog.Lines == null)
            {
                Debug.LogWarning("Dialog or dialog lines are not initialized!");
                return;
            }

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

    public IEnumerator ShowDialog(Dialog dialog, List<string> choices = null, Action<int> onChoiceSelected = null)
    {
        if (!dialogActive)
        {
            OnShowDialog?.Invoke();
            this.dialog = dialog;
            dialogBox.SetActive(true);
            dialogActive = true;

            if (dialog != null)
            {
                currentLine = 0;
                yield return StartCoroutine(TypeDialog(dialog.Lines[currentLine]));
            }
            else if (choices != null && choices.Count > 1)
            {
                yield return choiceBox.ShowChoices(choices, choiceIndex =>
                {
                    onChoiceSelected?.Invoke(choiceIndex);
                    if (choiceIndex == 0)
                    {
                        dialogBox.SetActive(false);
                        dialogActive = false;
                        OnHideDialog?.Invoke();
                    }
                });
            }
        }
    }

    public IEnumerator ShowDialogText(string text)
    {
        dialogBox.SetActive(true);
        dialogText.text = text;
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Z));
        dialogBox.SetActive(false);
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
