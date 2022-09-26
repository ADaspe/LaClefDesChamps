using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    public int HealAmount;
    [SerializeField] private PlayerState playerState;
    [SerializeField] private PlayerVFX playerVFX;

    public int MaxHealt = 10;
    //[HideInInspector]
    public int currenthealth;

    private void Start()
    {
        currenthealth = MaxHealt;
        //SetMaxhealth();
    }

    public void PlayerTakeDamage(int damage)
    {
        currenthealth = currenthealth - damage;
        Invoke("Dead", 1f);

        // magie noir pour le display des coeur ne fonctionnera pas quand on va reganer de la vie a moins de faire + de magie noir 
        UIManager.instance.currentHeart = currenthealth;
        UIManager.instance.damageAmount =  damage;
        UIManager.instance.DisplayHeart();
        //
            
        playerVFX.cameraShake(2f);
        if(currenthealth <= 2) { playerVFX.SetMaterialPorperty("_ClignotementON", 1); }   // 1 true , 0 false 
        Dead();
    }

    public bool Dead()
    {
        bool toReturn = false;
        if (currenthealth <= 0)  {playerState.ChangeState(PlayerState.PlayerStates.dead);
            toReturn = true;
            playerVFX.SetMaterialPorperty("_ClignotementON", 0);    // 1 true , 0 false 
        }
        else toReturn = false;

        return toReturn;
    }   // on peut srmt ecrire ca mieux

    // REGENE HP
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HP"))
        {
            if (currenthealth == MaxHealt) return;

            if (currenthealth < MaxHealt - HealAmount)
            {
                currenthealth = currenthealth + HealAmount;
                playerVFX.Heal();
                playerVFX.SetMaterialPorperty("_ClignotementON", 0);

                UIManager.instance.currentHeart = currenthealth;
                UIManager.instance.DisplayHeart();

                Destroy(other.gameObject);
            }

            else if (currenthealth >= MaxHealt - HealAmount)
            {
                currenthealth = MaxHealt;
                playerVFX.Heal();
                UIManager.instance.ResetHP();
                playerVFX.SetMaterialPorperty("__ClignotementON", 0);
                
                Destroy(other.gameObject);
            }

        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        { 
            PlayerTakeDamage(2);
            playerState.ChangeState(PlayerState.PlayerStates.hit);
        } // DEBUG
    }
}

