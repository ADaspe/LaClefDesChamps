using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Grab : MobMobState
{
    public override void EnterState(MobMob mob)
    {
        Debug.Log("I'm grabbed");
    }

    public override void Update(MobMob mob)
    {
        //Vector3.MoveTowards(mob.transform.position, mob.PlayerGrab.transform.position, mob.PlayerGrab.attackStats.grabSpeed);
        mob.Steer(mob.PlayerGrab.transform.position-mob.transform.position);
        if (Vector3.Distance(mob.transform.position, mob.PlayerGrab.transform.position) <= mob.PlayerGrab.attackStats.grabMinDistance)
        {
            mob.TransitionToState(mob.IdleState);
        }
    }
}
