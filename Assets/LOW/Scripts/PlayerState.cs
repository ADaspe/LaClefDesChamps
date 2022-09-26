using System.Collections;
using UnityEngine;

public class PlayerState : MonoBehaviour
{
    private InputReader InputReader;
    private AbsorbManager AbsorbManager;
    private CharacterController characterController;
    private CharacterAnimations characterAnimations;
    private NavCharacterController NavCharacterController;
    private SpecialAttackFilter specialAttackFilter;
    private PlayerVFX playerVFX;
    private PlayerHP playerHP;

    public bool isAttacking;
    private bool isMoving;
    public bool swapAtk;

    [SerializeField] Transform _spawnPoint;
    public enum PlayerStates
    {
        moving       = 1,
        Absorb       = 2,
        SpecialFire  = 3,
        SpecialMetal = 4,
        SpecialLight = 5,
        basicattack  = 6,
        hit,
        dead,
    }
    
    public PlayerStates currentState;
    public PlayerStates previousState;
    private void Start()
    {
        swapAtk = true; // alterener entre les 2 anims d'atk

        InputReader = GetComponent<InputReader>();
        AbsorbManager = GetComponent<AbsorbManager>();
        characterController = GetComponent<CharacterController>();
        NavCharacterController = GetComponent<NavCharacterController>();
        playerVFX = GetComponentInChildren<PlayerVFX>();
        playerHP = GetComponentInChildren<PlayerHP>();
        
        characterAnimations = GetComponentInChildren<CharacterAnimations>();
        specialAttackFilter = GetComponentInChildren<SpecialAttackFilter>();
    }   // only Scripts Setup 

    public void ChangeState(PlayerStates newstate)
    {
        previousState = currentState;

        currentState = newstate;

        switch (currentState)
        {
            case PlayerStates.SpecialFire:
                isAttacking = true;
                isMoving = false;
                specialAttackFilter.SetAttackPower(PlayerVFX.PowerType.fire);
                AbsorbManager.instance.powerChargesAmount--;
                characterAnimations.PlayAnimation();

                break;
            case PlayerStates.SpecialMetal:
                isAttacking = true;
                isMoving = false;
                specialAttackFilter.SetAttackPower(PlayerVFX.PowerType.metal);
                AbsorbManager.instance.powerChargesAmount--;
                characterAnimations.PlayAnimation();

                break;
            case PlayerStates.SpecialLight:
                isAttacking = true;
                isMoving = false;

                specialAttackFilter.SetAttackPower(PlayerVFX.PowerType.light);
                AbsorbManager.instance.powerChargesAmount--;
                characterAnimations.PlayAnimation();
                break;

            case PlayerStates.Absorb:
                isAttacking = true;
                isMoving = false;

                characterAnimations.PlayAnimation();
                specialAttackFilter.SetAttackPower(PlayerVFX.PowerType.absorb);     // vizuals
                break;

            case PlayerStates.basicattack:
                isAttacking = true;  //change in animation event 
                isMoving = false;
                    
                UIManager.instance.HitScreen(false);
                characterAnimations.PlayAnimation();
                break;

            case PlayerStates.hit:
                isAttacking = true;  //change in animation event 
                isMoving = false;

                UIManager.instance.HitScreen(true);
                playerVFX.SetMaterialPorperty("_hit",100);
                characterAnimations.PlayAnimation();
                break;

            case PlayerStates.dead:
                isMoving = false;
                UIManager.instance.DeathScreenIN();
                // TP dosen't work if called after RespawnPlayer() for some reasons :/
                Invoke("TpPlayer", .9f);
                Invoke("RespawnPlayer", 1);
                break;

            case PlayerStates.moving:
                playerVFX.SetMaterialPorperty("_hit", 1);
                UIManager.instance.HitScreen(false);
                isMoving = true;
                break;
                
            default:
                break;
        }
    }

    public void CheckPlayerStates() // look in the mobmob script to change the fct tres shady qu'il y ai besoin de l'input de special fire
    {
        if (playerHP.Dead()) return;   // filtre is dead

        if (isAttacking)     return;      // already channeling

        else if (InputReader.AbsorbInput())
        {
            ChangeState(PlayerStates.Absorb);
        }
        // BASIC ATK
        else if (InputReader.BasicAttackInput())
        {
            swapAtk = !swapAtk;     // swap betwenn 2 basic attack animation 
            specialAttackFilter.SetAttackPower(PlayerVFX.PowerType.Atk);   // vfx to basic atk
            ChangeState(PlayerStates.basicattack);
        }

        // SPECIAL ATTACK
        else if (InputReader.SpecialAttackInput() && AbsorbManager.instance.powerChargesAmount > 0)      // Don't overide if already attacking + can't attack if you don't have charges
        {
            // FIRE ATTACK
            if (AbsorbManager.instance.powerType == AbsorbManager.Elements.fire)
            {
                ChangeState(PlayerState.PlayerStates.SpecialFire);
            }

            // METAL ATTACK
            else if (AbsorbManager.instance.powerType == AbsorbManager.Elements.metal)
            {
                ChangeState(PlayerState.PlayerStates.SpecialMetal);
            }

            // LIGHT ATTACK
            else if (AbsorbManager.instance.powerType == AbsorbManager.Elements.light)
            {
                ChangeState(PlayerState.PlayerStates.SpecialLight);
            }
        }
        // IDLE
        else if(!isMoving) 
        {
            ChangeState(PlayerStates.moving); // le variable de type state prend pour valeur le state moving de player state
        }
    }
    
    private void Update()
    {
        CheckPlayerStates();
        
        if (Input.GetKeyDown(KeyCode.I)) { playerVFX.cameraShake(2f); } // DEBUG camera shake
    }

    private void TpPlayer() => transform.position = _spawnPoint.position;
    private void RespawnPlayer()
    {
        UIManager.instance.DeathScreenOut();
        UIManager.instance.ResetHP();
        playerHP.currenthealth = playerHP.MaxHealt;
    }
}