using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;

public class AXD_HitTrigger : MonoBehaviour
{
    public PlayerController playerController;
    private BoxCollider AttackZoneTrigger;
    public LayerMask layersToHit;

    private void Start()
    {
        AttackZoneTrigger = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("J'ai tapé " + other.gameObject.name+" et son layer est "+ LayerMask.LayerToName(other.gameObject.layer));
        if (other.gameObject.layer == layersToHit)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                if (playerController.currentHitCombo == 1)
                {
                    other.GetComponent<Enemy_Core>().InvokeDamage(playerController.attackStats.damageATK1, playerController.transform.position, playerController.attackStats.stunTimeATK1);
                }
                else if (playerController.currentHitCombo == 2)
                {
                    other.GetComponent<Enemy_Core>().InvokeDamage(playerController.attackStats.damageATK2, playerController.transform.position, playerController.attackStats.stunTimeATK2);
                }
                else if (playerController.currentHitCombo == 3)
                {
                    other.GetComponent<Enemy_Core>().InvokeDamage(playerController.attackStats.damageATK3, playerController.transform.position, playerController.attackStats.stunTimeATK3);
                }

            }
            else if (other.gameObject.layer == LayerMask.NameToLayer("DestructibleOnHit"))
            {

            }
        }
    }
}
