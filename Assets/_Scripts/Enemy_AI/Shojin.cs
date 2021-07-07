using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shojin : MonoBehaviour
{
    #region Shojin Variables
    [Header("Properties")]
    public CharacterController characterController = null;
    public Enemy_Core enemy_Core = null;

    [HideInInspector] public GameObject player;

    [Header("Steering Settings")]
    public float force = 0;
    [Range(1f, 5f)]
    public float speed = 0;

    [Header("Monster Settings")]
    public int lifePoints = 10;
    public int baseAttackValue = 1;

    [Header("Patrol Settings")]
    public Transform[] wayPoints;


    #endregion

    #region Shojin State
    ShojinState currentState;
    Shojin_Idle IdleState = new Shojin_Idle();
    #endregion
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        player = GameObject.FindWithTag("Player");
    }

    void Start()
    {
        enemy_Core.onDamage += Damage;


        TransitionToState(IdleState);
    }

    void Update()
    {
        currentState.Update(this);
    }

    public void TransitionToState(ShojinState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    #region Damage Methods
    private void Damage(int damageAmount, Vector3 damageSource)
    {
        lifePoints -= damageAmount;
    }

    private void Death()
    {

    }
    #endregion

    private void OnDestroy()
    {
        enemy_Core.onDamage -= Damage;
        //enemy_Core.onDeath -= Death;
    }
}
