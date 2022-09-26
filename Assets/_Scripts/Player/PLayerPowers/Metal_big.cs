using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Metal_big : MonoBehaviour
{
    [SerializeField] PlayerVFX playerVFX;
    private HPmanager HPmanager;
    private MobMobState mobMobState;
    private MobMobVisualManager mobMobVisualManager;

    [SerializeField] Collider MetalDamageCollider;

    [Header("           Metal Attack parameters")]
    public LayerMask             DamagableEntities;     // not used mdr

    [SerializeField] private float _shockwavedTime;
    public int                   metalDamageAmount;

    public void EnableCollider()
    {   
        MetalDamageCollider.enabled = true;
        StartCoroutine(ResteCollider());
        if (AbsorbManager.instance.powerChargesAmount == 0) { AbsorbManager.instance.SetPower(0, AbsorbManager.Elements.empty); }
        playerVFX.cameraShake(4f);

        // UI 
        UIManager.instance.SwapPowerIcone();
        UIManager.instance.compteurCharges++;
        UIManager.instance.ChargeAmount();  
    }

    private void OnTriggerEnter(Collider mobmob)  // try to damage collider that are not mobmobs !!
    {
        if (mobmob.CompareTag("MobMob"))
        {
            mobmob.GetComponent<HPmanager>().TakeDamage(metalDamageAmount); // called in the PlayerVFX && collider deactivated 
            mobmob.GetComponent<MobMobState>().shockwavedTime = _shockwavedTime;

            mobmob.GetComponent<MobMobState>().ChangeState(MobMobState.MobStates.shockwaved); // Set state to burning
            if (mobmob.GetComponentInChildren<MobMobVisualManager>() != null) { mobmob.GetComponentInChildren<MobMobVisualManager>().MobPlayVFX(); }
        }
    }

    // pas eu le temps 
    public void KnockBackEnnemy()
    {
        //// deactive nav mesh + add force to rb 
        //foreach (Collider MobMob in MetalDetectEnnemy())
        //{
        //    Vector3 KnockBackDir;
        //    KnockBackDir = (_sphereDetectCenter - MobMob.transform.position).normalized / 10;

        //    MobMob.transform.Translate(transform.position + KnockBackDir); // not super effective :'(
        //    print(KnockBackDir);
        //}
    }

    IEnumerator ResteCollider()     // is it realy clean ?
    {
        // n'a pa fonctionner avec waitendofframe
        yield return new WaitForSeconds(1f);      // arbitray value to reset the collider as disabled (the best way would be to let it live only one frame)
        MetalDamageCollider.enabled = false;
    }
}