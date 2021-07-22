using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Release : PlayerState
{
    public override void EnterState(PlayerController player)
    {
        //Subscribe to the event
        OnRelease_Relayer.onRelease += OnRelease;
        OnRelease_Relayer.onEndAnim += OnEndAnim;

        //Play Animation
        player.animator.SetTrigger("Release");
        // /!\Don't forget to unsubscribe from the event !!
        
    }

    private void OnRelease(PlayerController player)
    {
        //player.animator.SetBool("isReleasing", false);
        if(player.absorbDebug) Debug.Log("[Player Release] Releasing element...");
        player.book.ReleaseElement();
    }

    private void OnEndAnim(PlayerController player)
    {
        player.TransitionToState(player.IdleState);
        OnRelease_Relayer.onRelease -= OnRelease;
        OnRelease_Relayer.onEndAnim -= OnEndAnim;
    }

    #region UnusedMethods
    public override void UpdateState(PlayerController player)
    {

    }

    public override void FixedUpdate(PlayerController player)
    {

    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {
       
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
        
    }
    #endregion

}
