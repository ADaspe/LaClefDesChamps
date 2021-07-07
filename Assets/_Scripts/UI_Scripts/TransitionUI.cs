using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using TransitionManagement;

public class TransitionUI : MonoBehaviour
{
    private Animator animator;
    private TransitionManager transitionManager = null;

    private void Awake()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    public void UISetTransition()
    {
        Debug.Log("[TransitionUI] Set Transition");
        animator.SetBool("OnFade", true);
    }

    public void UIFireTransition()
    {
        animator.SetBool("OnFade", false);
        if(transitionManager == null)transitionManager = FindObjectOfType<TransitionManager>();
        transitionManager.FireTransition();
    }

}

//[System.Serializable] public class LevelChangeEvent : UnityEvent<string> { }
