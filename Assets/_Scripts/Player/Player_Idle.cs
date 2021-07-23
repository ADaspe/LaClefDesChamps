using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Idle : PlayerState
{
    public override void EnterState(PlayerController player)
    {
        if(player.debug)Debug.Log("Entering Idle State...");
    }

    public override void UpdateState(PlayerController player)
    {
        player.Move();

        //Enter Attack State on Attack Button Down
        if (Input.GetButtonDown("Fire1"))
        {
            if(player.debug)Debug.Log("Fire button down");
            player.TransitionToState(new Player_Attack());
        }

        //Enter Interact if player is Near an Interractible element
        if (Input.GetButtonDown("Fire2"))
        {
            if(player.debug) Debug.Log("Interract button down");
            Collider[] hitColliders = Physics.OverlapSphere(player.transform.position, player.interractionRange, player.interractibleLayer, QueryTriggerInteraction.Collide );
            foreach (var collider in hitColliders)
            {
                Debug.Log(collider.gameObject);
            }
        }
        

        if(Input.GetButtonDown("Fire3"))
        {
            //Check if smthg is inside the book
            if (player.book.GetElement() == null)
            {
                player.TransitionToState(new Player_Absorb());
            }
            else
            {
                if (player.book.GetElement()._element == Element.Frog)
                {
                    player.TransitionToState(new Player_Grapple());
                }
                else
                    player.TransitionToState(new Player_Release());
            }
                
        }
    }

    public override void FixedUpdateState(PlayerController player)
    {
        
    }

    public override void OnTriggerEnter(PlayerController player, Collider c)
    {
        /*
        if (player.CheckEnemyCollision(c))
        {
            player.TransitionToState(player.HurtState);
        }*/
           
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
        //throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        //throw new System.NotImplementedException();
    }

}
