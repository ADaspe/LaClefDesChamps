using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Chasing : MobMobState
{

    public override void EnterState(MobMob mob)
    {
        //Debug.Log("[Mob Mob] Entering Chasing State");
        mob.animator.SetFloat("Blend", 1f);
    }

    public override void Update(MobMob mob)
    {
        Vector3 acc = SetInterest(mob);
        mob.Steer(acc);
        
        if (mob.DetectPlayer(mob.nearPlayerRadius)) mob.TransitionToState(mob.PatrolState);
        if(!mob.DetectPlayer(mob.awareRadius * 1.2f)) mob.TransitionToState(mob.IdleState);

    }

    private Vector3 SetInterest(MobMob mob)
    {
        Vector3 chosenDir = Vector3.zero;
        //If the Mob-Mob is chasing, then the maximum interest is the vector towards the Player
        Vector3 towardPlayer = mob.player.transform.position - mob.transform.position;
        Vector3.Normalize(towardPlayer);

        for (int i = 0; i < mob.interest.Length; i++)
        {
            float dotProduct = Vector3.Dot(mob.ray_dir[i], towardPlayer);

            mob.interest[i] = Mathf.Max(0, dotProduct);

            //Debug.DrawRay(this.transform.position, ray_dir[i], Color.green);
        }

        //Minus interest towards obstacle
        for (int i = 0; i < mob.danger.Length; i++)
        {
            bool isDangerous = Physics.Raycast(mob.transform.position, mob.ray_dir[i], 5f);
            Debug.DrawRay(mob.transform.position, mob.ray_dir[i] * 10, Color.blue);
            if (isDangerous) mob.interest[i] = mob.interest[i] * -1f;
            
            chosenDir += mob.ray_dir[i] * mob.interest[i];
        }

        chosenDir = chosenDir.normalized;
        return chosenDir;
    }
}
