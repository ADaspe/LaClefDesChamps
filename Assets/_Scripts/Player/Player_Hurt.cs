using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Hurt : PlayerState
{
    private float evaluateTime = 0;
    private bool stopKnockback = false;
    public override void EnterState(PlayerController player)
    {
        Debug.Log("Entering Hurt State...");
        player.animator.SetTrigger("Hurt");

        //Play hurt FX ?

    }

    public override void UpdateState(PlayerController player)
    {
        evaluateTime += Time.deltaTime;
        if(!stopKnockback)Knockback(player);

        if (evaluateTime >= player.knockbackTime)
            stopKnockback = true;
        if (evaluateTime >= player.hurtTime)
        {
            player.animator.ResetTrigger("Hurt");
            player.TransitionToState(player.IdleState);
        }
    }
    private void Knockback(PlayerController player)
    {
        //Calculate the knockback force
        float force = Mathf.Lerp(0, player.maxKnockbackForce, player.knockbackCurve.Evaluate(evaluateTime));

        player.Knockback(player.knockDirection, force);
    }

    #region Unused Methods


    public override void FixedUpdateState(PlayerController player)
    {

    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {
       
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
        //throw new System.NotImplementedException();
    }
    #endregion
}
