using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMobAnimationManager : MonoBehaviour
{
    private MobMobState mobMobState;
    private MobMobController mobMobController;
    public enum AnimState
    {
        movement = 0,
        attack = 1,
        hit = 2,
    }
    public AnimState animState;

    public Animator mobAnimator;

    void Start()
    {
        mobMobState = GetComponentInParent<MobMobState>();
        mobMobController = GetComponentInParent<MobMobController>();
    }

    public void pauseAnimation(bool ispaused) => mobAnimator.gameObject.SetActive(ispaused); 

    public void playMobAnimation()
    {
        switch (animState)
        {
            case AnimState.attack:
                //print("AnimState is Attacking");
                mobAnimator.SetBool("moving", false);  
                mobAnimator.SetTrigger("Attack");
                break;

            case AnimState.hit:
                //mobAnimator.SetTrigger("Hurt");
                mobAnimator.SetBool("moving", false);
                break;

            case AnimState.movement:
                //print("AnimState is MOVEMENT");
                mobAnimator.SetBool("moving", true); 
                break;

            default:
                break;
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) { pauseAnimation(true); print("m pressed"); }  // big wtf
    }
}
