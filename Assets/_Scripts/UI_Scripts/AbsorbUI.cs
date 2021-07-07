using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Player;

public class AbsorbUI : MonoBehaviour
{
    [SerializeField] private Sprite fireIcon = null;
    [SerializeField] private Sprite frogIcon = null;
    [SerializeField] private Sprite lightIcon = null;
    [SerializeField] private Sprite metalIcon = null;
    [SerializeField] private Image icon = null;

    private void Start()
    {
        NoIcon();
    }

    public void SetIcon(AbsorbElement element)
    {
        if(element._element == Element.Fire)
        {
            icon.sprite = fireIcon;
        }
        if (element._element == Element.Frog)
            icon.sprite = frogIcon;
        if (element._element == Element.Light)
            icon.sprite = lightIcon;
        if (element._element == Element.Metal)
            icon.sprite = metalIcon;
        icon.enabled = true;
    }

    public void NoIcon()
    {
        icon.sprite = null;
        icon.enabled = false;
    }
}
