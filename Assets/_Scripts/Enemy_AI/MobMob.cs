using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob : MonoBehaviour
{
    #region Mob Variables
    [Header("Properties")]
    public CharacterController characterController = null;
    public Enemy_Core enemy_Core = null;
    public Animator animator = null;

    [Space(10f)]
    [Header("Steering Settings")]
    public float force = 0;
    [Range(1f, 5f)]
    public float speed = 0;
    public float animationBlend = 2f;

    [Space(10f)]
    [Header("Detection Settings")]
    [SerializeField] private int nbr_rays = 8;
    public float awareRadius = 1f;
    public float nearPlayerRadius = 1f;
    public float chaseBackMargin = 2f;
    public Transform target;

    /*
    [Space(10f)]
    [Header("Wandering Settings")]
    [Range(0.8f, 6f)]
    public float minWanderTime = 1f;
    [Range(0.8f, 6f)]
    public float maxWanderTime = 1f;
    public AnimationCurve idleTime;*/

    [Space(10f)]
    [Header("Patrol Settings")]
    [Tooltip("The MobMob will pick its next destination between those two angles, every reevaluateTime seconds")]
    [Range(1f, 180f)]
    public float minAngle = 1f;
    [Range(1f, 180f)]
    public float maxAngle = 1f;
    [Range(0f, 2f)]
    public float reevaluateTime = 1f;
    [Space(10f)]
    [Tooltip("The MobMob will attack when its attack rate crosses the attack Treshold")]
    [Range(0f, 2f)]
    public float minAttackTreshold;
    [Range(0f, 2f)]
    public float maxAttackTreshold;
    [Range(0f, 2f)]
    public float attackTimeStep;
    [Range(0f, 2f)]
    public float attackRateStep;
    [Space(10f)]
    [Header("Attacks Settings")]
    public Transform attackPoint;
    public Vector3 attackDimension;
    public float attackDuration = 0f;
    public float attackDelay;
    [Tooltip("The number of detected collision in order to count the attack as 'hit'.")]
    public int attackTreshold = 3;
    public LayerMask playerLayer;
    /*public LayerMask enemyLayer;
    public LayerMask playerAttackLayer;*/
    public GameObject attackTrail;
    public bool attackDebug = true;
    [Space(15f)]
    [Header("Hurt Settings")]
    public AnimationCurve knockbackCurve;
    public float maxKnockbackForce = 0f;
    public float knockbackTime = 0f;
    public float hurtTime = 0f;
    public float invincibilityTime = 0f;
    public bool isStun;
    public float timeToBeStunned;
    public float timeToCancelStun;
    [HideInInspector] public Vector3 knockDirection;
    [HideInInspector] public bool canHurt = true;

    [Header("Fire DoT settings")]
    public bool isOnFire;
    public float timeToEndFire;
    public int damagePerTick;
    public float tickPerSecond;

    [Header("FX")]
    public GameObject hurtFx;
    public GameObject deathFX;

    [Header("Monster Settings")]
    public int lifePoints = 10;
    public int baseAttackValue = 1;
    #endregion

    [HideInInspector] public GameObject player;

    #region Steering Variables
    [HideInInspector] public float[] interest;
    [HideInInspector] public float[] danger;
    [HideInInspector] public Vector3[] ray_dir;     //Forward is Forward

    [HideInInspector] public Vector3 velocity = Vector3.zero;
    #endregion

    #region MobMob State
    public bool stateDebug;
    private MobMobState currentState;
    public MobMob_Idle IdleState = new MobMob_Idle();
    public MobMob_Chasing ChasingState = new MobMob_Chasing();
    public MobMob_Patrol PatrolState = new MobMob_Patrol();
    public MobMob_Stun StunState = new MobMob_Stun();
    #endregion


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
    }
    void Start()
    {
        interest = new float[nbr_rays];
        danger = new float[nbr_rays];
        ray_dir = new Vector3[nbr_rays];

        for(int i = 0; i < ray_dir.Length; i++)
        {
            float angle = i * (360/nbr_rays);
            
            Quaternion myRotation = Quaternion.AngleAxis(angle, new Vector3(0,1,0));
            Vector3 startingDirection = Vector3.forward;
            Vector3 result = myRotation * startingDirection;

            Vector3.Normalize(result);
            ray_dir[i] = result;
        }

        enemy_Core.onDamage += Damage;
        //enemy_Core.onDeath += Death;

        TransitionToState(IdleState);

        canHurt = true;
        //SetInterestTowardsPlayer();
    }

    public void TransitionToState(MobMobState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
    
    void Update()
    {
        currentState.Update(this);
    }

    public void LookAtTarget(Vector3 target)
    {

        Vector3 lookPos = target - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 3f);
        
    }

    public bool DetectPlayer(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider c in hitColliders)
        {
            if(c.CompareTag("Player"))
            {
                return true;
            }
            
        }
        return false;
    }

    public void Steer(Vector3 direction)
    {
        Vector3 steering = direction * force;
        steering.y = 0;

        if (!characterController.isGrounded)
            steering += Physics.gravity;
        characterController.Move(steering * Time.deltaTime * speed);
    }


    public void Knockback(Vector3 incomingDir, float force)
    {
        incomingDir = incomingDir.normalized;
        incomingDir = incomingDir * force;

        //ADD GRAVITY TROU DU CUL

        characterController.Move(incomingDir);
    }

    /*private void SetInterest()
    {
       

        //------IF PATROLLING----
        if(currentState == MONSTER_STATE.Patrol)
        {
            //If the Monster is patrolling, then the maximum interest is the direction allowing him to stay at the same distance
        }
   
        //------IF IDLE----------
        if(currentState == MONSTER_STATE.Idle)
        {

        }

    }*/

    #region Damage Methods
    private void Damage(int damageAmount, Vector3 damageSource, float stunTime = 0)
    {
        if (canHurt)
        {
            damageSource.y = transform.position.y;

            Vector3 knocbackDirection = (damageSource - transform.position) * -1;
            knocbackDirection = knocbackDirection.normalized;
            knockDirection = knocbackDirection;
            if(stunTime != 0)
            {
                timeToBeStunned = stunTime;
                isStun = true;
                timeToCancelStun = Time.time + stunTime;
            }
            lifePoints -= damageAmount;
            canHurt = false;
            if(lifePoints <= 0)
            {
                TransitionToState(new MobMob_Death());
            }
            else TransitionToState(new MobMob_Hurt());
        }
    }

    public void SetOnFire(int damagePerTick, float time, float frequency)
    {
        StopCoroutine(FireCoroutine());
        isOnFire = true;
        this.damagePerTick = damagePerTick;
        timeToEndFire = Time.time + time;
        tickPerSecond = frequency;
        StartCoroutine(FireCoroutine());

    }

    IEnumerator FireCoroutine()
    {
        Damage(damagePerTick, transform.position);
        while (Time.time < timeToEndFire)
        {
            yield return new WaitForSeconds(1/tickPerSecond);
        }
        isOnFire = false;
    }

    private void ResetStun()
    {
        isStun = false;
    }
    #endregion

    private void OnDestroy()
    {
        enemy_Core.onDamage -= Damage;
        //enemy_Core.onDeath -= Death;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(attackPoint.position, attackDimension);
    }

}
