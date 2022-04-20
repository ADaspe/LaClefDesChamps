using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMob_FxShowcase : MonoBehaviour
{
    [SerializeField] Animator Mobanimator;

    Renderer rend;
    
    [Header("MATERIALS")]
    [SerializeField]Material[] Materials;  

    [Header ("VFX Parameters")]
    [SerializeField] float BurnTime;
    [SerializeField] float BlindTime;
    [SerializeField] float ShockwavedTime;
    [SerializeField] float HitTime;

    [Header("VFX")]
    [SerializeField] GameObject BurnFx;
    [SerializeField] GameObject BurnHitFx;
    [SerializeField] GameObject LightFx;
    [SerializeField] GameObject HitFx;

    // Instanciate point
    /*
    [SerializeField] Transform BurnPoint;
    [SerializeField] Transform HitPoint;
    [SerializeField] Transform LightPoint;
    */

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = Materials[0];
    }

    void MobBurn()
    {
        if (MC_FxShowcase.instance.IsOnFire)
        {
            // Animation
            Mobanimator.Play("");

            // Swap to Burn Mat
            rend.sharedMaterial = Materials[1];

            // Activate VFX
            BurnFx.SetActive(true);
            BurnHitFx.SetActive(true);

            //Start Coroutine to deactivate VFX effects
            StartCoroutine(BurningDelay());
        }
    }
    void MobLight()
    {
        if (MC_FxShowcase.instance.IsBlinded)
        {
            // Swap to Blind Mat
            rend.sharedMaterial = Materials[2];

            // Activate VFX
            LightFx.SetActive(true);

            //Start Coroutine to deactivate VFX effects
            StartCoroutine(BlindedDelay());
        }
    }
    void MobShockwaved()
    {
        if(MC_FxShowcase.instance.IsShockwaved)
        { 
        // Swap to Stun Mat
        rend.sharedMaterial = Materials[2];

        //Start Coroutine to deactivate VFX effects
        StartCoroutine(ShockwavededDelay());
        }
    }
    void MobHit()
    {
        if(MC_FxShowcase.instance.IsHit)
        {
            // Animation
            //Mobanimator.SetTrigger("Hurt");

            // Swap to Hit Mat
            rend.sharedMaterial = Materials[3];

            // VFX
            HitFx.SetActive(true);

            //Deactivate VFX
            StartCoroutine(HitDelay());
        }
    }
    void Update()
    {
        // Default Mat
        if(!MC_FxShowcase.instance.IsBlinded && !MC_FxShowcase.instance.IsBlinded && !MC_FxShowcase.instance.IsShockwaved)
        {
            rend.sharedMaterial = Materials[0];
        }

        // MobMob Visual imperment
        MobBurn();
        MobLight();
        MobShockwaved();
        MobHit();
    }

    // Coroutines 
    IEnumerator BurningDelay()
    {
        yield return new WaitForSeconds(BurnTime);
        MC_FxShowcase.instance.IsOnFire = false;
        BurnFx.SetActive(false);
        BurnHitFx.SetActive(false);
    }
    IEnumerator BlindedDelay()
    {
        yield return new WaitForSeconds(BlindTime);
        MC_FxShowcase.instance.IsBlinded = false;
        LightFx.SetActive(false);
    }
    IEnumerator ShockwavededDelay()
    {
        yield return new WaitForSeconds(ShockwavedTime);
        MC_FxShowcase.instance.IsShockwaved = false;
    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(HitTime);
        MC_FxShowcase.instance.IsHit = false;
        HitFx.SetActive(false);
    }
}
