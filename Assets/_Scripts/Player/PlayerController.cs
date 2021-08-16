using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Player
{

    public class PlayerController : MonoBehaviour
    {
        #region Player Variables

        [Header("Components")]
        public Animator animator = null;
        public Renderer[] renderers;
        public Camera mainCamera;
        [Header ("General Settings")]
        [SerializeField] private float speed = 0;
        [SerializeField] private float animationBlend = 2f;
        public bool applyGravity = true;

        public bool debug = true;

        [Header("Absorb Settings")]
        public PlayerBook book;
        public float absorbDuration;
        public bool absorbDebug = true;
        public GameObject AbsorbPoint;
        [Header("Absorb Debug")]
        public float AbsorbRangeWidth;
        public float AbsorbRangeHeight;
        public float AbsorbRangeDepth;
        public LayerMask DetectedLayers;


        [Header("Hurt Settings")]
        public AnimationCurve knockbackCurve;
        public float maxKnockbackForce = 0f;
        public float knockbackTime = 0f;
        public float hurtTime = 0f;
        public float invincibilityTime = 0f;
        [HideInInspector] public Vector3 knockDirection;

        private bool canMove = true;

        [Header("Attacks Settings")]
        public AttackSettingsSO attackStats;
        public Transform attackPoint;
        public Vector3 attackDimension;
        public float attackDuration = 0f;
        public int attackDamage = 1;
        [Tooltip("The number of detected collision in order to count the attack as 'hit'.")]
        public int attackTreshold = 3;
        public LayerMask enemyAttackLayer;
        public LayerMask enemyLayer;
        public LayerMask playerAttackLayer;
        public LayerMask burnLayers;
        public bool attackDebug;
        public int currentHitCombo = 1;
        public float lastHitTime;

        [Header("Interractible Settings")]
        public LayerMask interractibleLayer;
        public float interractionRange = 4f;

        [Header("Collectibles")]
        public int pears;
        public int seeds;

        [Header("Fx")]
        public GameObject attackTrail;
        public float attackTrailOffset = 0.2f;
        public GameObject absorbFX;
        public float absorbOffset = 0.1f;
        //private event<Action> onInterract;

        [Header("TestFX")]
        public GameObject testFireFX;
        public GameObject testAbsorbFX;
        public GameObject testHealFX;
        public GameObject testShieldFX;
        public GameObject testFireBlastConeFX;

        [HideInInspector] public CharacterController playerController;

        private Vector3 lastDirection = new Vector3(0, 0, 0);
        public HealthSystem playerHealth;
        #endregion

        #region PlayerStates
        private PlayerState currentState;
        public Player_Idle IdleState;
        public Player_Hurt HurtState;
        public Player_Attack AttackState;
        //public Player_Grapple GrappleState = new Player_Grapple();
        #endregion


        private void Awake()
        {
            playerController = gameObject.GetComponent<CharacterController>();
            enemyAttackLayer = LayerMask.GetMask("EnemyAttacks");
            enemyLayer = LayerMask.GetMask("Enemy");
            interractibleLayer = LayerMask.GetMask("Interractible");
            attackTrail.SetActive(false);
            playerHealth = new HealthSystem(5);
            IdleState = new Player_Idle();
            HurtState = new Player_Hurt();
            AttackState = new Player_Attack();
            
            //playerAnimator = gameObject.GetComponentInChildren<Animator>();
        }

        void Start()
        {
            TransitionToState(IdleState);
        }
        void Update()
        {
            currentState.UpdateState(this);
        }

        private void FixedUpdate()
        {
            currentState.FixedUpdateState(this);
        }

        private void OnTriggerEnter(Collider c)
        {
            currentState.OnTriggerEnter(this, c);
        }

        private void OnTriggerStay(Collider c)
        {
            currentState.OnTriggerStay(this, c);
        }

        private void OnTriggerExit(Collider c)
        {
            currentState.OnTriggerExit(this, c);
        }

        public void TransitionToState(PlayerState state)
        {
            currentState = state;
            currentState.EnterState(this);
        }


        #region Custom Methods
        public void Move()
        {
            if (canMove)
            {
                float x = Input.GetAxisRaw("Horizontal");
                float z = Input.GetAxisRaw("Vertical");

                Vector3 movement = new Vector3(x, 0, z).normalized;
                
                Vector3 rotatedMovement = Quaternion.Euler(0, mainCamera.transform.rotation.eulerAngles.y, 0) * movement;
                
                if (movement.magnitude >= 0.1)
                {
                    lastDirection = rotatedMovement;
                    float blendTarget = 1f;
                    animator.SetFloat("Blend", Mathf.Lerp(animator.GetFloat("Blend"), blendTarget, animationBlend * Time.deltaTime));
                }
                else
                {
                    float blendTarget = 0f;
                    animator.SetFloat("Blend", Mathf.Lerp(animator.GetFloat("Blend"), blendTarget, animationBlend * Time.deltaTime));
                }

                //Debug.Log("Last Direction : " + lastDirection);
                
                transform.LookAt(transform.position + lastDirection);

                //Gravity
                if (applyGravity)
                {
                    if (!playerController.isGrounded)
                    {
                        rotatedMovement += Physics.gravity;
                    }
                }
                
                playerController.Move(rotatedMovement * Time.deltaTime * speed);
                
            }
        }

        public void Knockback(Vector3 incomingDir, float force)
        {
            incomingDir = incomingDir.normalized;
            incomingDir = incomingDir * force;

            playerController.Move(incomingDir);
        }


        public void DamagePlayer(int damageAmount, Vector3 damageSource)
        {
            damageSource.y = transform.position.y;

            Vector3 knocbackDirection = (damageSource - transform.position) * -1;
            knocbackDirection = knocbackDirection.normalized;
            knockDirection = knocbackDirection;

            playerHealth.Damage(damageAmount);

            TransitionToState(new Player_Hurt());
        }

        public void HealPlayer(int healAmount)
        {
            playerHealth.Heal(healAmount);
        }

        public void BlastCone()
        {
            Collider[] detectedBurnable = Physics.OverlapSphere(book.gameObject.transform.position, attackStats.maxDistanceDetectionATK3Fire, burnLayers);
            Debug.Log("J'ai détecté " + detectedBurnable.Length + " items.");
            StartCoroutine(BlastConeCoroutine(this));
            foreach (Collider detectedItem in detectedBurnable)
            {
                //Si le machin détecté est à moins de maxAngleDetectionATK3Fire° => s'il est dans le cône de feu
                if (Vector3.Dot(lastDirection, (detectedItem.transform.position - book.transform.position).normalized) >= Mathf.Cos(attackStats.maxAngleDetectionATK3Fire*Mathf.Rad2Deg))
                {
                    if(detectedItem.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                    {
                        detectedItem.GetComponent<MobMob>().SetOnFire(attackStats.damagePerTickATK3Fire, attackStats.dotTimeATK3Fire,attackStats.tickPerSecondATK3Fire);
                    }
                }
            }

        }

        public void AddPear(int number = 1)
        {
            pears += number;
        }

        public void AddSeed(int number = 1)
        {
            seeds += number;
        }
        #endregion


        public IEnumerator FireCoroutine(PlayerController player)
        {
            yield return new WaitForSeconds(2);
            player.testFireFX.SetActive(false);
        }

        public IEnumerator AbsorbCoroutine(PlayerController player)
        {
            yield return new WaitForSeconds(2);
            player.testAbsorbFX.SetActive(false);
        }
        public IEnumerator HealCoroutine(PlayerController player)
        {
            yield return new WaitForSeconds(5);
            player.testHealFX.SetActive(false);
        }
        public IEnumerator ShieldCoroutine(PlayerController player)
        {
            yield return new WaitForSeconds(5);
            player.testShieldFX.SetActive(false);
        }
        
        public IEnumerator BlastConeCoroutine(PlayerController player)
        {
            testFireBlastConeFX.SetActive(true);
            yield return new WaitForSeconds(1);
            testFireBlastConeFX.SetActive(false);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(attackPoint.position, attackDimension);
        }
    }
}
