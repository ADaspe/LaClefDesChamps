using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Idle : MobMobState
{
    public override void EnterState(MobMob mob)
    {
        if (mob.stateDebug)
        {
            Debug.Log("[Mob Mob] Entering Idle State");
        }
        mob.animator.SetFloat("Blend", 0f);
    }

    public override void Update(MobMob mob)
    {

        bool nearPlayer = mob.DetectPlayer(mob.awareRadius + mob.nearPlayerRadius);
        if(nearPlayer)
        {
            mob.LookAtTarget(mob.player.transform.position);
        }

        if (mob.DetectPlayer(mob.awareRadius))
        {
            mob.TransitionToState(mob.ChasingState);
        }
    }
}
