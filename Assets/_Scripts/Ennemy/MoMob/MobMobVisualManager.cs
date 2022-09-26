using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMobVisualManager : MonoBehaviour
{
    [SerializeField] Transform camPos;
    [SerializeField] Transform mo;
    [SerializeField] MobMobAttack mobAttack;
    [SerializeField] GameObject Exclamation;
    MobMobState MobMobState;
    MobMobController mobMobController;

    [SerializeField] Renderer rend;

    [Header("MATERIALS")]
    [SerializeField] Material[] Materials;

    [Header("VFX")]
    // SHOULD MAKE THE PS AND PLAY AND STOP CAUSING BUGS
    public ParticleSystem BurnFx;
    [SerializeField] ParticleSystem BurnHitFx;
    [SerializeField] ParticleSystem LightFx;
    [SerializeField] ParticleSystem HitFx;
    [SerializeField] ParticleSystem DeathtFx;
    [SerializeField] GameObject AttackVFX;

    public Transform[] VFXPoints;
    // VISUALS
    public GameObject[] MobMobVisuals;

    public bool isBruning;

    void Start()
    {
        MobMobState      = GetComponentInParent<MobMobState>();
        mobMobController = GetComponentInParent<MobMobController>();
        rend.enabled = true;
        rend.sharedMaterial = Materials[0];
    }
    public void StopAllVFX()
    {
        HitFx.Stop(true);
        //BurnFx.gameObject.SetActive(false);
        //BurnHitFx.gameObject.SetActive(false);
        LightFx.Stop(true);
    }
    void MobShockwaved()
    {
        rend.material = Materials[2]; // no visiual ?? try another Mat maybe 
        print("mob swap mat");
    }
    void AttackFX(bool isactive) => AttackVFX.SetActive(isactive);
    public void exclamation()
    {
        Exclamation.SetActive(true);
    }   
    public void MobNormal()
    {
        // Swap to Normal MAT
        if (!isBruning) { rend.sharedMaterial = Materials[0]; }

        // Activate VFX
        StopAllVFX();
    }   
    public void Dead()
    {
        StopAllVFX();
        DeathtFx.Play(true);
    }       
    public void MobBurn(bool isactive)
    {
        // Swap to Burn Mat
        rend.material = Materials[1];

        // Activate VFX
        BurnFx.gameObject.SetActive(isactive);
        BurnHitFx.gameObject.SetActive(isactive);
        isBruning = isactive;
    }
    public void MobHit()
    {
        // Animation
        //Mobanimator.SetTrigger("Hurt");
        // Swap to Hit Mat
        rend.sharedMaterial = Materials[2];

        // VFX
        Object.Instantiate(HitFx, VFXPoints[0].position, VFXPoints[0].rotation);
    }
    void MobLight()
    {
           // Swap to Blind Mat
            rend.sharedMaterial = Materials[2];

            // Activate VFX
            LightFx.Play(true);
    }
    // coroutine vace float + mat a reset pour reste les mat 
    public void MobPlayVFX()
    {
        switch (MobMobState.mobStates)
        {
            case MobMobState.MobStates.idle:
                MobNormal();
                break;
            case MobMobState.MobStates.wander:
                MobNormal();
                break;
            case MobMobState.MobStates.chase:
                MobNormal();
                break;
            case MobMobState.MobStates.attack:
                mobAttack.EnableCollider();
                AttackFX(true);
                break;
            case MobMobState.MobStates.hit:
                MobHit();
                print("mobmob VFX HIT");
                break;
            //case MobMobState.MobStates.burning:
            //    MobBurn();
            //    break;
            case MobMobState.MobStates.shockwaved:
                MobShockwaved();
                break;
            case MobMobState.MobStates.lightstun:
                MobLight();
                break;
            default:
                break;
        }
    }
    public void EndAttack()
    {
        MobMobState.isAttack = false;
        AttackFX(false);
    }
}