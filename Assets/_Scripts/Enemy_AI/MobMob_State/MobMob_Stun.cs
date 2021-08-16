using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Stun : MobMobState
{
    public override void EnterState(MobMob mob)
    {
        if (mob.stateDebug)
        {
            Debug.Log("[Mob Mob] Entering Stun State");
        }
        //Appliquer anim de stun
        //Appliquer FX
        //Debug.Log("Aïe je suis stun pour " + mob.timeToBeStunned + " secondes");
    }

    public override void Update(MobMob mob)
    {
        if (Time.time >= mob.timeToCancelStun)
        {
            mob.TransitionToState(mob.IdleState);
        }
    }
}
