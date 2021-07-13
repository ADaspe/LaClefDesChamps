using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : PlayerState
{

    public AttackSettingsSO attackStats;
    private float attackDuration;
    private Dictionary<Collider, int> attackDictionary = new Dictionary<Collider, int>();
    private bool anyHit;
    
    public override void EnterState(PlayerController player)
    {
        if(player.attackDebug) Debug.Log("[Player State] Entering Attack State...");
        
        attackDuration = player.attackDuration;
        player.animator.SetBool("isAttacking", true);
        anyHit = false;

    }

    public override void Update(PlayerController player)
    {
        Attack(player);
        ApplyAttack(player);

        float baseAttackDuration = player.attackDuration;
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
        }
    }

    public override void FixedUpdate(PlayerController player)
    {
        
    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {

    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {

    }

    #region Attack Methods
    private void Attack(PlayerController player)
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
    }
    #endregion

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }
    

}
