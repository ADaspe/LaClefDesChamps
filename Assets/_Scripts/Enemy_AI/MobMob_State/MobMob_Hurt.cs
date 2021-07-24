using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Hurt : MobMobState
{
    private float evaluateTime = 0;
    private bool stopKnockback = false;

    public override void EnterState(MobMob mob)
    {
        mob.animator.SetTrigger("Hurt");
        mob.hurtFx.SetActive(true);
    }

    public override void Update(MobMob mob)
    {
        Debug.Log("Aïe je me suis fait taper");
        evaluateTime += Time.deltaTime;
        if (!stopKnockback) Knockback(mob);

        if (evaluateTime >= mob.knockbackTime)
            stopKnockback = true;
        if (evaluateTime >= mob.hurtTime)
        {
            mob.animator.ResetTrigger("Hurt");
            mob.hurtFx.SetActive(false);
            mob.canHurt = true;
            mob.TransitionToState(mob.StunState);
        }
    }

    private void Knockback(MobMob mob)
    {
        //Calculate the knockback force
        float force = Mathf.Lerp(0, mob.maxKnockbackForce, mob.knockbackCurve.Evaluate(evaluateTime));

        mob.Knockback(mob.knockDirection, force);
    }
}
