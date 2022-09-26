using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBasicCombat : MonoBehaviour
{
    private HPmanager HPmanager;
    private MobMobState mobMobState;
    public PlayerVFX PlayerVFX;
    [SerializeField] private PlayerState playerState;

    [SerializeField] Collider Atkcollider;  // deux collider sur chaque maina  la base puis un seul et seul l'anim swap
    [SerializeField] int baseDamage;

    public void EnableCollider()
    {
        Atkcollider.enabled = true;
        StartCoroutine(ResteCollider());    // if mob dosen't hot the collider stay enable;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MobMob"))     // check for collider Tag
        {
            other.GetComponent<HPmanager>().TakeDamage(baseDamage); // dit qu'il doit se prendre des dmg uniquement une fois 
            
            if (!other.GetComponent<MobMobState>().Impared)
            {
                other.GetComponent<MobMobState>().ChangeState(MobMobState.MobStates.hit);
            }
            else
            {
                other.GetComponentInChildren<MobMobVisualManager>().MobHit();
            }

            if (other.GetComponentInChildren<MobMobVisualManager>() != null) { other.GetComponentInChildren<MobMobVisualManager>().MobPlayVFX(); }
            PlayerVFX.cameraShake(3f);
            Atkcollider.enabled = false;   // only damage one mob 
        }
    }
    IEnumerator ResteCollider()     // is it realy clean ?
    {
        yield return new WaitForSeconds(.01f);
        Atkcollider.enabled = false;
    }
}