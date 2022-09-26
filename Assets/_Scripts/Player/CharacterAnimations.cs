using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimations : MonoBehaviour
{
    [SerializeField]  UCharacterController uCharacterController;
    [SerializeField]  PlayerState playerState;
    public Animator PlayerAnimator;

    public void PlayAnimation()
    {
        switch (playerState.currentState)
        {
            case PlayerState.PlayerStates.moving:
                PlayerAnimator.SetFloat("Speed", uCharacterController.playerVelocity);    
                PlayerAnimator.SetBool("Moving", true);

                break;

            case PlayerState.PlayerStates.basicattack:
                //swap between to animations    
                PlayerAnimator.SetBool("Moving", false);
                if (playerState.swapAtk)
                {
                    PlayerAnimator.SetTrigger("isBasicAtk1");
                }
                else
                {
                    PlayerAnimator.SetTrigger("isBasicAtk2");
                }
                break;
            
            case PlayerState.PlayerStates.hit:
                PlayerAnimator.SetBool("Moving", false);
                PlayerAnimator.SetTrigger("isHit");
                break;
                
            case PlayerState.PlayerStates.Absorb:
                PlayerAnimator.SetBool("Moving", false);
                PlayerAnimator.SetTrigger("isAbsorb");
                break;

            case PlayerState.PlayerStates.SpecialFire:
                PlayerAnimator.SetBool("Moving", false);
                PlayerAnimator.SetTrigger("isFire");
                break;

            case PlayerState.PlayerStates.SpecialMetal:
                PlayerAnimator.SetBool("Moving", false);
                PlayerAnimator.SetTrigger("isMetal");
                break;

            case PlayerState.PlayerStates.SpecialLight:
                PlayerAnimator.SetBool("Moving", false);
                PlayerAnimator.SetTrigger("isLight");
                break;

            default:
                break;
        }
    }
}
