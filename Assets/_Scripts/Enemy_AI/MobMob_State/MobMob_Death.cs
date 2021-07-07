using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Death : MobMobState
{
    private float evaluateTime = 0;
    private bool stopKnockback = false;
    public override void EnterState(MobMob mob)
    {
        mob.animator.SetTrigger("Hurt");
        mob.deathFX.SetActive(true);
    }

    public override void Update(MobMob mob)
    {
        evaluateTime += Time.deltaTime;
        if (!stopKnockback) Knockback(mob);

        if (evaluateTime >= mob.knockbackTime)
            stopKnockback = true;
        if (evaluateTime >= mob.hurtTime)
        {
            mob.animator.ResetTrigger("Hurt");
            mob.hurtFx.SetActive(false);
            mob.canHurt = true;
            //mob.TransitionToState(mob.IdleState);
            mob.gameObject.SetActive(false);
        }
    }

    private void Knockback(MobMob mob)
    {
        //Calculate the knockback force
        float force = Mathf.Lerp(0, mob.maxKnockbackForce, mob.knockbackCurve.Evaluate(evaluateTime));

        mob.Knockback(mob.knockDirection, force);
    }
}
