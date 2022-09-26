using UnityEngine;

public class MC_FxShowcase : MonoBehaviour
{
    [SerializeField] Animator MCAnimator;

    public string Currentstate;

    public static MC_FxShowcase instance;

    [HideInInspector] public bool IsOnFire;
    [HideInInspector] public bool IsBlinded;
    [HideInInspector] public bool IsShockwaved;
    [HideInInspector] public bool IsHit;

    #region Player Animations 
    public const string PLAYER_ATK1 = ("MC Rig_MC atk_1 0");
    public const string PLAYER_ATK2 = ("MC Rig_MC atk_2 0");
    public const string PLAYER_ATK3 = ("MC Rig_MC atk_nopower");
    public const string PLAYER_DMG = ("MC Rig_MC damage"); 
    public const string PLAYER_POWER = ("MC Rig_MC use_power"); 
    public const string PLAYER_DIALOGUE = ("MC Rig_MC interaction_dialogue");
    public const string PLAYER_ABSORB = ("MC Rig_MC absorb");
    public const string PLAYER_IDLE = ("MC Rig_MC_Idle");
    public const string PLAYER_FIRE = ("MC Rig_MC special_fire 0");
    public const string PLAYER_LIGHT = ("MC Rig_MC special_light 0");
    public const string PLAYER_METAL = ("MC Rig_MC special_metal 0");
    #endregion
    
    void Awake()
    {
        instance = this;
    }
    public void PlayerAnimationState(string newState)
    {
        if (Currentstate == newState) return;

        MCAnimator.Play(newState);

        Currentstate = newState;
    }

    // Player Attacks
    void PLayerAttack1()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            PlayerAnimationState(PLAYER_ATK1);
        }
    }
    void PLayerAttack2()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            PlayerAnimationState(PLAYER_ATK2);
        }
    }
    void PlayerAttack3()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            PlayerAnimationState(PLAYER_ATK3);
        }
    }

    //Special Attacks
    void PlayerSpecialFire()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            // Animation
            PlayerAnimationState(PLAYER_FIRE);
        }
    }
    void PlayerSpecialMetal()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            PlayerAnimationState(PLAYER_METAL);
        }
    }
    void PlayerSpecialLight()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Animation
            PlayerAnimationState(PLAYER_LIGHT);
        }
    }

    // Player absorb element
    void PlayerAbsorbElement()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            // Animation
            PlayerAnimationState(PLAYER_ABSORB);
        }
    }

    // PLayer use power
    void PlayerUseFire()
    {
        if(Input.GetKeyDown(KeyCode.J))
        {
            PlayerAnimationState(PLAYER_POWER);
        }
    }

    // Player Hurt
    void PlayerIsHurt()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            // Animation
            PlayerAnimationState(PLAYER_DMG);
        }
    }
    
    // Player Interactions
    void PlayerDialogue()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            // Animation
            PlayerAnimationState(PLAYER_DIALOGUE);
        }
    }
   
    // MobMob Effects
    void SetOnfire()
    {
        IsOnFire = true;
    }
    void SeBlinded()
    {
        IsBlinded = true;
    }
    void SetShockwaved()
    {
        IsShockwaved = true;
    }
    void SetHit()
    {
        IsHit = true;
    }

    void Update()
    {
        //Base Attacks
        PLayerAttack1();
        PLayerAttack2();
        PlayerAttack3();
        
        //Special Attacks
        PlayerSpecialLight();
        PlayerSpecialMetal();
        PlayerSpecialFire();

        // Player absorb element
        PlayerAbsorbElement();

        // Use Power
        PlayerUseFire();

        // Player hurt
        PlayerIsHurt();

        //Player interaction
        PlayerDialogue();
    }
}
