using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogUI : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private TMP_Text textPanel = null;
    [SerializeField] private TMP_Text namePanel = null;
    [SerializeField] private GameObject pressKeyPanel = null;
    [SerializeField] private GameObject allPanels = null;

    private string currentText = string.Empty;
    private string currentName = string.Empty;
    private string targetText;
    private string nextLetter = string.Empty;

    private bool isTyping = false;

    private void Start()
    {
        //DisplayText("Un deux, Un deux, test...", "Admin");
        //allPanels = GameObject.Find("Pannels");
    }

    public void DisplayText(string text, string name, float typingSpeed)
    {
        
        ResetText();
        namePanel.text = name;
        currentName = name;

        targetText = text;
        StartCoroutine(Typing(typingSpeed));

        ToggleDialogPanel(true);
    }

    private IEnumerator Typing(float typingSpeed)
    {
        isTyping = true;

        for (int i = 0; i < targetText.Length; i++)
        {
            nextLetter = targetText.Substring(i, 1);
            currentText = currentText + nextLetter;
            textPanel.text = currentText;
            yield return new WaitForSeconds(typingSpeed);
 
        }

        EndingDialogue();

    }

    private void EndingDialogue()
    {
        if (isTyping == true)
        {
            isTyping = false;
        }
    }

    public void ResetText()
    {
        currentName = string.Empty;
        currentText = string.Empty;
    }

    #region Toggle Methods
    public void ToggleDialogPanel(bool active)
    {
        if (allPanels != null)
            allPanels.SetActive(active);
        else print("Erreur, références aux pannels manquantes");
    }

    public void ToggleSpacebarPanel(bool active)
    {
        pressKeyPanel.SetActive(active);
    }
    #endregion




}
