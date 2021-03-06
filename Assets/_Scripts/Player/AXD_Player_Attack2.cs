using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Player_Attack2 : PlayerState
{
    private float stateTime;
    private bool bufferedX;
    private bool bufferedY;

    public override void EnterState(PlayerController player)
    {
        player.currentHitCombo = 2;
        stateTime = Time.time;
    }

    public override void FixedUpdateState(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(PlayerController player)
    {
        //Anim de coup 2
        //Passer au coup 3 en fonction de l'élément
        if (Input.GetButtonDown("Fire1"))
        {
            player.StartCoroutine(BufferXCoroutine(player));
        }
        if (Input.GetButtonDown("UseElement"))
        {
            player.StartCoroutine(BufferYCoroutine(player));
        }
        if (Time.time - stateTime <= player.attackStats.MaxInputDelayATK2)
        {
            if (bufferedX)
            {
                player.TransitionToState(player.Attack3State);
            }
            else if (bufferedY)
            {
                if (player.book.currentElement.GetType() == typeof(FireElement))
                {
                    player.TransitionToState(player.Attack3FireState);
                }
                else if (player.book.currentElement.GetType() == typeof(MetalElement))
                {
                    player.TransitionToState(player.Attack3MetalState);
                }
                else if (player.book.currentElement.GetType() == typeof(FrogElement))
                {
                    player.TransitionToState(player.Attack3FrogState);
                }
                else if (player.book.currentElement.GetType() == typeof(FireflyElement))
                {
                    player.TransitionToState(player.Attack3FireflyState);
                }
            }
        }
        else
        {
            player.TransitionToState(player.IdleState);
        }
    }

    IEnumerator BufferXCoroutine(PlayerController player)
    {
        bufferedX = true;
        yield return new WaitForSeconds(player.InputBufferToleranceSeconds);
        bufferedX = false;
    }

    IEnumerator BufferYCoroutine(PlayerController player)
    {
        bufferedY = true;
        yield return new WaitForSeconds(player.InputBufferToleranceSeconds);
        bufferedY = false;
    }
}
