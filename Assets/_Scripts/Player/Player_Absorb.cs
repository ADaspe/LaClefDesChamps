using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Absorb : PlayerState
{
    private float absorbDuration;
    public override void EnterState(PlayerController player)
    {
        //Play Absorb Animation
        player.animator.SetTrigger("Absorb");

        absorbDuration = player.absorbDuration;
        Absorb(player);

        //player.absorbFX.SetActive(true);
    }

    public override void UpdateState(PlayerController player)
    {
        //float baseAbsorbDuration = absorbDuration;
        if (absorbDuration > 0)
        {
            absorbDuration -= Time.deltaTime;
            player.animator.ResetTrigger("Absorb");

            if (absorbDuration < player.absorbDuration - player.absorbOffset)
            {
                player.absorbFX.SetActive(true);
            }
        }
        else
        {
            player.absorbFX.SetActive(false);
            player.TransitionToState(player.IdleState);
        }
    }

    public void Absorb(PlayerController player)
    {

        if (player.absorbDebug) Debug.Log("[Player_Absorb] Il n'y a pas d'éléments absorbables contenus dans le livre pour l'instant");
        //If nothing is inside the book, check if there is a source near
        //Modify the starting point of the sphere to be in front of the book instead of in the player
        Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, player.interractionRange, player.interractibleLayer, QueryTriggerInteraction.Collide);
        foreach (var c in hitColliders)
        {
            if (player.absorbDebug) Debug.Log("[Player_Absorb] Collider touché (Layer Interraction) = " + c);
             ElementSource nearSource = c.gameObject.GetComponent<ElementSource>();

            if (nearSource != null)
            {
                //Then, if there is one Absorb it
                nearSource.AbsorbSource(player.book);
                break;
            }
        }
    }

    #region Unused Methods
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
