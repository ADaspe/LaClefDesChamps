using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMobState : MonoBehaviour
{
    [SerializeField] Win_KIllCount win_kIllCount;
    private MobMobVisualManager MobMobVisualManager;
    private MobMobAnimationManager MobMobAnimationManager;
    private MobMobController mobMobController;
    [SerializeField] Collider mobMobCollider;
    [SerializeField] Looter looter;
    
    public bool playerDetected;
    public bool isHit;
    public bool isBurning;
    public bool Dead;
    public bool isAttack = false;
    public bool canAttack = false;
    public bool Impared;

    [HideInInspector] public float burningTime;
    [HideInInspector] public float shockwavedTime;
    [HideInInspector] public float hitTime;
    [HideInInspector] public float lightTime;

    [Header("Charecater parameters")]
    public int attackDamages;
    public float attackCD = 2f;

    [Header("IA parameters")]
    public float suspectRange = 15f;
    public float detectionRange = 10f;
    public float minimumDetectRange = 2f;   // not used
    public float attackTriggerRange = 2f;
    public float wanderTime = 1f;

    public enum MobStates
    {
        idle = 0,
        wander = 1,
        attack = 2,
        hit = 3,
        burning = 4,
        shockwaved = 5,
        lightstun = 6,
        dead = 7,
        chase =8
    }

    public MobStates mobStates;
    public MobStates _previousState;

    private void Start()
    {
        MobMobVisualManager = GetComponentInChildren<MobMobVisualManager>();
        MobMobAnimationManager = GetComponentInChildren<MobMobAnimationManager>();
        mobMobController = GetComponent<MobMobController>();
        canAttack = true;
}

    private void trackStateChanges()
    {
        // ne pas rentrer en perma dans les states 
        if (isAttack) return;
        else if (inAttackRange() && canAttack) { ChangeState(MobStates.attack); }

        else if (inChaseRange() && !isAttack && playerDetected) { ChangeState(MobStates.chase); }
        
        else if (!inChaseRange() && !isAttack && !playerDetected) { ChangeState(MobStates.wander); }
    }

    public void SetAnimState(MobMobAnimationManager.AnimState nextanimState)
    {
        //print("change anim state");
        MobMobAnimationManager.animState = nextanimState;
        MobMobAnimationManager.playMobAnimation();
    }

    public void ChangeState (MobStates nextState)
    {
       _previousState = mobStates;

        mobStates = nextState;
  
        switch (mobStates)
        {
            case MobStates.idle:
                SetAnimState(MobMobAnimationManager.AnimState.movement);    
                MobMobAnimationManager.playMobAnimation();
                MobMobVisualManager.MobPlayVFX();
                break;

            case MobStates.wander:
                MobMobVisualManager.MobPlayVFX();
                mobMobController.WanderAround();
                break;
            
            case MobStates.chase:
                if(ennemyDetectionIcon()) { MobMobVisualManager.exclamation(); }
                SetAnimState(MobMobAnimationManager.AnimState.movement);
                MobMobVisualManager.MobPlayVFX();
                mobMobController.ChaseSpeeds();
                mobMobController.CloseToPLayer();
                break;

            case MobStates.attack:
                canAttack = false;  // link to attack CD
                isAttack = true;    // link to changing state conditions
                StartCoroutine(AttackCD());

                SetAnimState(MobMobAnimationManager.AnimState.attack);
                mobMobController.navMeshAgent.velocity = Vector3.zero;
                break;

            case MobStates.hit:
                isAttack = true;
                MobMobAnimationManager.playMobAnimation();
                StartCoroutine(SpecialHitState(hitTime));
                SetAnimState(MobMobAnimationManager.AnimState.hit);
                print("mobmob HIT");
                break;

            case MobStates.burning:
                // don't work big ouin 
                //isAttack = true;
                MobMobVisualManager.MobBurn(true);
                StartCoroutine(SpecialHitState(burningTime));
                break;

            case MobStates.shockwaved:
                isAttack = true;
                MobMobAnimationManager.pauseAnimation(true);
                StartCoroutine(SpecialHitState(shockwavedTime));    
                break;
            case MobStates.lightstun:
                isAttack = true;
                Impared = true;
                StartCoroutine(SpecialHitState(lightTime));
                break;

            case MobStates.dead:
                isAttack = true;
                looter.Loot();
                MobMobVisualManager.Dead();
                StartCoroutine(Death(2));
                mobMobController.navMeshAgent.velocity = Vector3.zero;
                break;
            default:
                break;
        }
    }   
    private void Update()
    {
        trackStateChanges();
    }
    IEnumerator SpecialHitState(float VFXtime)
    {
        yield return new WaitForSeconds(VFXtime);
        isAttack = false;   // put it in hte anim instead
        MobMobVisualManager.MobBurn(false);
        ChangeState(MobStates.wander);
    }
    IEnumerator Death(float WaitBeforedeactivate)
    {   
        // hide MobMob mesh
        MobMobVisualManager.MobMobVisuals[0].SetActive(false);
        MobMobVisualManager.MobMobVisuals[1].SetActive(false);
        mobMobCollider.enabled = false;
        // then deactivate the GO   
        yield return new WaitForSeconds(WaitBeforedeactivate);
        win_kIllCount.updateWin();
        gameObject.SetActive(false);
    }
    IEnumerator AttackCD()
    {
        yield return new WaitForSeconds(attackCD + 1);
        canAttack = true;
    }
    private bool inAttackRange() => mobMobController.distanceToTarget < attackTriggerRange;
    private bool ennemyDetectionIcon() => _previousState != mobStates && _previousState != MobStates.attack && mobMobController.distanceToTarget >= 4;
    public bool inChaseRange()  => mobMobController.distanceToTarget < suspectRange;
    public bool closeToPlayer()  => mobMobController.distanceToTarget < 2.3f;
}