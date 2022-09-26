using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMobAttack : MonoBehaviour
{
    [SerializeField] MobMobState mobMobState;

    [SerializeField] Collider _attackCollider;

    public void EnableCollider()
    {
        _attackCollider.enabled = true;
        Invoke("DiseableCollider", .02f);
    }

    // output damage when mob is in attack state 
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLayer"))
        {
            other.GetComponent<PlayerHP>().PlayerTakeDamage(mobMobState.attackDamages);
            if(other.GetComponent<PlayerState>() != null) 
            {
                other.GetComponent<PlayerState>().ChangeState(PlayerState.PlayerStates.hit); // Set state to burning
            }
            DiseableCollider();
        }
    }
    private void DiseableCollider() { _attackCollider.enabled = false; }
}