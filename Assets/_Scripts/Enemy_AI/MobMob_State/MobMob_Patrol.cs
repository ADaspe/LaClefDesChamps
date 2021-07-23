using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_Patrol : MobMobState
{
    
    private Vector3 target = Vector3.zero;
    private bool hasTarget = false;

    private float attackRate = 0f;
    private float attackTreshold = 2f;
    public override void EnterState(MobMob mob)
    {
        //Debug.Log("[Mob Mob] Entering Patrol State");
        attackRate = 0f;
        attackTreshold = 0f;

        EvaluateAttackTreshold(mob);
        mob.StartCoroutine(IncreaseAttackProbability(mob.attackTimeStep, mob.attackRateStep));
    }

    public override void Update(MobMob mob)
    {
        mob.LookAtTarget(mob.player.transform);
        if (!hasTarget) mob.StartCoroutine(SetInterest(mob));

        //Steer in the direction of the target;
        Vector3 direction = target - mob.transform.position;
        direction = direction.normalized;
        mob.Steer(direction);

        //Animation Blend
        float blendTarget = 1f;
        mob.animator.SetFloat("Blend", Mathf.Lerp(mob.animator.GetFloat("Blend"), blendTarget, mob.animationBlend * Time.deltaTime));

        //Evaluate if its time to attack
        if (EvaluateAttack(mob)) mob.TransitionToState(new MobMob_Attack());

        //If the player is too far Away, start chasing
        if (!mob.DetectPlayer(mob.nearPlayerRadius + mob.chaseBackMargin))
        {
            mob.CancelInvoke();
            mob.TransitionToState(mob.ChasingState);
        }
    }

    private bool EvaluateAttack(MobMob mob)
    {

        if(attackRate > attackTreshold)
        {
            return true;
        }
        else return false;
    }

    private void EvaluateAttackTreshold(MobMob mob)
    {  
        attackTreshold = Random.Range(mob.minAttackTreshold,mob.maxAttackTreshold);
        //Debug.Log("[Mob Patrol] Evaluate Attack Treshold = " + attackTreshold);
    }
    private IEnumerator IncreaseAttackProbability(float timeStep, float rateStep)
    {
        while (attackTreshold > attackRate)
        {
            attackRate += rateStep;
            //Debug.Log("[Mob Patrol] Increase Attack Probability = " + attackRate);
            yield return new WaitForSeconds(timeStep);
        }
    }

    private Vector3 CalculateTargetAngle(Transform center, MobMob mob, float angle)
    {
        //Make sure the center pos & mobmob pos are at the same height by zeroing out their y axis

        Vector3 centerCorrectPos = new Vector3(center.position.x, mob.transform.position.y, center.position.z);
        //Vector3 mobCorrectPos = new Vector3(mob.transform.position.x, mob.transform.position.y, mob.transform.position.z);

        float radius = Vector3.Distance(centerCorrectPos, mob.transform.position);

        float x = Mathf.Abs(center.position.x - mob.transform.position.x);
        float z = Mathf.Abs(center.position.z - mob.transform.position.z);


        float initialAngle = Mathf.Atan2(z, x) * Mathf.Rad2Deg;

        float desiredAngle = (initialAngle + angle) * Mathf.Deg2Rad;

        float xResult = radius * Mathf.Cos(desiredAngle);
        float zResult = radius * Mathf.Sin(desiredAngle);

        if (mob.transform.position.x < center.transform.position.x)
        {
            xResult = xResult * -1;
        }

        if (mob.transform.position.z < center.transform.position.z)
        {
            zResult = zResult * -1;
        }

        //Get the target Pos back at the mobmob height
        Vector3 targetPos = new Vector3(center.transform.position.x + xResult, mob.transform.position.y, center.transform.position.z + zResult);

        //Debug.DrawLine(mob.transform.position, targetPos);
        Debug.DrawLine(centerCorrectPos, mob.transform.position, Color.red);
        Debug.DrawLine(centerCorrectPos, targetPos, Color.green);
        return targetPos;
    }

    private void EvaluateTarget(MobMob mob)
    {
        float angle = Random.Range(mob.minAngle, mob.maxAngle);

        //0.5 probability to invert the angle
        float rand = Random.Range(0, 1f);
        if (rand <= 0.5f) angle = -angle;

        target = CalculateTargetAngle(mob.player.transform, mob, angle);
    }

    IEnumerator SetInterest(MobMob mob)
    {
        hasTarget = true;
        EvaluateTarget(mob);
        yield return new WaitForSeconds(mob.reevaluateTime);
        hasTarget = false;
    }
}
