using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire_big : MonoBehaviour
{
    [SerializeField] PlayerVFX playerVFX;
    private HPmanager   HPmanager;
    private MobMobState mobMobState;
    private MobMobVisualManager mobMobVisualManager;

    private List<Collider> Targets = new List<Collider>();      // not used ask shy i didn't worked

    public Collider FireDamageCollider;

    [Header("           Fire Attack parameters") ]
    public int fireInstantdamages;
    
    // pas reussit a faire les degats en DOT
    public int fireDOTdamages;
    public int DOTtickAmount = 3;
    public float fireDOTtime;
    float tickLifetime;  // temps mtn + le temps 1 tick 

    public void EnableCollider()
    {
        FireDamageCollider.enabled = true;
        StartCoroutine(ResteCollider());
        // si il n'y a plus de charge remetre le state a empty charge reste 0 et state devient empty 
        if (AbsorbManager.instance.powerChargesAmount == 0) { AbsorbManager.instance.SetPower(0, AbsorbManager.Elements.empty); }
        playerVFX.cameraShake(4f);

        // UI 
        UIManager.instance.SwapPowerIcone();
        UIManager.instance.compteurCharges++;   // magie noir 
        UIManager.instance.ChargeAmount(); 
    }

    private void OnTriggerEnter(Collider other)   
    {
        if (other.CompareTag("MobMob"))
        {
            other.GetComponent<HPmanager>().TakeDamage(fireInstantdamages); // dit a MobMob tu brule et il doit se prendre des dmg uniquement une fois 
            other.GetComponent<HPmanager>().compteur = 0;
            other.GetComponent<HPmanager>().FireDOT(DOTtickAmount, fireDOTdamages);

            if (other.GetComponent<MobMobState>() != null)
            {
                other.GetComponent<MobMobState>().burningTime = fireDOTtime;
                other.GetComponent<MobMobState>().ChangeState(MobMobState.MobStates.burning); // Set state to burning
                other.GetComponentInChildren<MobMobVisualManager>().MobPlayVFX();
            }
           
            StartCoroutine(ResteCollider());
        }
        else if (other.CompareTag("flammable"))
        {
            other.GetComponent<InteractilbeObjects>().BurnObject();
            FireDamageCollider.enabled = false;
        }
    }   
    IEnumerator ResteCollider()     // is it realy clean ?
    {
        yield return new WaitForSeconds(.1f);      // arbitray value to reset the collider as disabled (the best way would be to let it live only one frame)
        FireDamageCollider.enabled = false;
    }
}