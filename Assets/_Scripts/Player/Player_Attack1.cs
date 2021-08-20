﻿using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack1 : PlayerState
{
    private float stateTime;
    private Dictionary<Collider, int> attackDictionary = new Dictionary<Collider, int>();
    private bool anyHit;
    
    public override void EnterState(PlayerController player)
    {
        stateTime = Time.time;
        if (player.attackDebug)
        {
            Debug.Log("[Player State] Entering Attack State for the hit " + player.currentHitCombo);
            Debug.Log("Current Hit Combo : " + player.currentHitCombo);
            Debug.Log("Time : " + Time.time + " LastHitTime : " + player.lastHitTime);
            Debug.Log("Temps depuis le dernier coup ? " + (Time.time - player.lastHitTime));
            Debug.Log("A eu le temps ? " + (Time.time - player.lastHitTime <= player.attackStats.MaxInputDelayATK3));
        }
        /*if(player.currentHitCombo == 3 && Time.time - player.lastHitTime <= player.attackStats.MaxInputDelayATK3)
        {
            Debug.Log("Coup 3");
            player.currentHitCombo = 1;
            player.lastHitTime = Time.time;
            //Anim de coup 3

        } 
        else if (player.currentHitCombo == 2 && Time.time - player.lastHitTime <= player.attackStats.MaxInputDelayATK2)
        {
            Debug.Log("Coup 2");
            player.currentHitCombo = 3;
            player.lastHitTime = Time.time;
            //Anim de coup 2

        } 
        else
        {
            Debug.Log("Coup 1");
            player.currentHitCombo = 2;
            player.lastHitTime = Time.time;
            //Anim de coup 1

        }*/

        player.animator.SetBool("isAttacking", true);
        anyHit = false;

    }

    public override void UpdateState(PlayerController player)
    {

        //Jouer l'anim
        //Ne pas oublier le buffering


        /*float baseAttackDuration = player.attackDuration;
        if(attackDuration < baseAttackDuration - player.attackTrailOffset)
        {
            player.attackTrail.SetActive(true);
            player.attackTrail.GetComponentInChildren<ParticleSystem>().Play();
        }

        if (attackDuration >= 0)
        {
            attackDuration -= Time.deltaTime;
            player.animator.SetBool("isAttacking", false);
        }
        else 
        {

            player.attackTrail.SetActive(false);
            player.TransitionToState(player.IdleState); 
        }*/
    }

    public override void FixedUpdateState(PlayerController player)
    {
        
    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {

    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {

    }

    #region Attack Methods
    /*private void Attack(PlayerController player)
    {
        Collider[] enemyHits = Physics.OverlapBox(player.attackPoint.position, player.attackDimension, player.attackPoint.rotation, player.enemyLayer);


        foreach (Collider c in enemyHits)
        {
            if (attackDictionary.ContainsKey(c))
            {
                attackDictionary[c]++;
            }
            else
            {
                attackDictionary.Add(c, 1);
            }
        }

    }

    private void ApplyAttack(PlayerController player)
    {
        List<Enemy_Core> enemyHit = new List<Enemy_Core>();

        foreach (var col in attackDictionary)
        {
            if(player.attackDebug)Debug.Log("[Apply Attack] Collider : " + col + "Number of contact : " + col.Value);
            if(col.Value > player.attackTreshold)
            {
                enemyHit.Add(col.Key.gameObject.GetComponent<Enemy_Core>());
            }
        }

        for(int i =0; i < enemyHit.Count; i++)
        {
           if(player.attackDebug)Debug.Log("[Apply Attack] Enemy hit : " + enemyHit[i].name);
            enemyHit[i].InvokeDamage(player.attackDamage, player.animator.transform.position);
        }

        anyHit = true;
    }*/
    #endregion

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }
    

}