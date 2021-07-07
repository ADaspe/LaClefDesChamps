using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialogManager : Singleton<DialogManager>
{
    [SerializeField] private DialogUI dialogUI;

    void Start()
    {
        dialogUI = FindObjectOfType<DialogUI>();
        //dialogUI.DisplayText("Bridging Cinematic and Gameplay...", "Toinon");
    }

    public void SetText(string text, string name, float typingSpeed)
    {
        dialogUI.ToggleDialogPanel(true);
        dialogUI.DisplayText(text, name, typingSpeed);

    }

    public void ResetText()
    {
        dialogUI.ResetText();
        dialogUI.ToggleDialogPanel(false);
        dialogUI.ToggleSpacebarPanel(false);
    }

}
