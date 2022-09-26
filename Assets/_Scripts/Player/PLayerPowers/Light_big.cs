using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light_big : MonoBehaviour
{
    [SerializeField] PlayerVFX playerVFX;
    private HPmanager HPmanager;   
    private MobMobState mobMobState;
    private MobMobVisualManager mobMobVisualManager;

    [SerializeField] Collider lightDamageCollider;

    [Header("           light Attack parameters")]
    public LayerMask DamagableEntities;     // not used mdr

    [SerializeField] private float _lightTime;
    public int lightDamageAmount;

    public void EnableCollider()
    {
        lightDamageCollider.enabled = true;
        StartCoroutine(ResteCollider());
        if (AbsorbManager.instance.powerChargesAmount == 0) { AbsorbManager.instance.SetPower(0, AbsorbManager.Elements.empty); }
        playerVFX.cameraShake(3f);

        // UI 
        UIManager.instance.SwapPowerIcone();
        UIManager.instance.compteurCharges++;
        UIManager.instance.ChargeAmount();
        print("collider ennabled");
    }

    private void OnTriggerEnter(Collider mobmob)  // try to damage collider that are not mobmobs !!
    {
        if (mobmob.CompareTag("MobMob"))
        {
            mobmob.GetComponent<HPmanager>().TakeDamage(lightDamageAmount); // called in the PlayerVFX && collider deactivated 
            mobmob.GetComponent<MobMobState>().lightTime = _lightTime;

            mobmob.GetComponent<MobMobState>().ChangeState(MobMobState.MobStates.lightstun); // Set state to burning
            if (mobmob.GetComponentInChildren<MobMobVisualManager>() != null) { mobmob.GetComponentInChildren<MobMobVisualManager>().MobPlayVFX(); }
        }
    }

    IEnumerator ResteCollider()     // is it realy clean ?
    {
        // n'a pa fonctionner avec waitendofframe
        yield return new WaitForSeconds(1f);      // arbitray value to reset the collider as disabled (the best way would be to let it live only one frame)
        lightDamageCollider.enabled = false;
    }
}
