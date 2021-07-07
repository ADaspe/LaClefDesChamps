using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class MobMob_Attack : MobMobState
{
    private float attackDuration = 0;
    private float attackDelay = 0;
    private float timer = 0;
    private bool playerHit = false;
    private Dictionary<Collider, int> attackDictionary = new Dictionary<Collider, int>();
    public override void EnterState(MobMob mob)
    {
        Debug.Log("[MobMob] Entering Attack State");
        attackDuration = mob.attackDuration;
        attackDelay = mob.attackDelay;
        mob.animator.SetTrigger("Attack");

    }

    public override void Update(MobMob mob)
    {
        timer += Time.deltaTime;
        if (timer >= attackDelay && !playerHit)
        {
            Attack(mob);
            ApplyAttack(mob);
        }


        float baseAttackDuration = mob.attackDuration;
        if (attackDuration < baseAttackDuration - (baseAttackDuration * 0.2))
        {
            mob.attackTrail.SetActive(true);
        }

        if (attackDuration > 0 )
        {
            attackDuration -= Time.deltaTime;
        }
        else
        {

            mob.attackTrail.SetActive(false);
            mob.animator.ResetTrigger("Attack");

            //Transition back to Patrol State
            mob.TransitionToState(mob.PatrolState);
        }
    }

    private void Attack(MobMob mob)
    {
        Collider[] playerHit = Physics.OverlapBox(mob.attackPoint.position, mob.attackDimension, mob.attackPoint.rotation, mob.playerLayer);

        foreach (Collider c in playerHit)
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

    private void ApplyAttack(MobMob mob)
    {

        foreach (var col in attackDictionary)
        {
            if (mob.attackDebug) Debug.Log("[MobMob / Apply Attack] Collider : " + col + "Number of contact : " + col.Value);
            
            if (col.Value > mob.attackTreshold)
            {
                playerHit = true;
            }
        }

        if(playerHit)
        {
            mob.player.GetComponent<PlayerController>().DamagePlayer(mob.baseAttackValue, mob.transform.position);
        }

    }
}
