using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_HitTrigger : MonoBehaviour
{
    public GameObject PlayerObject;
    public BoxCollider fistsCollider;
    public Player_Attack playerAttack;

    private void Start()
    {
        fistsCollider = GetComponent<BoxCollider>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy") )
        {
            if(playerAttack.currentHitCombo == 1)
            {
                other.GetComponent<Enemy_Core>().InvokeDamage(playerAttack.attackStats.damageATK1, PlayerObject.transform.position, playerAttack.attackStats.stunTimeATK1);
            }
            else if (playerAttack.currentHitCombo == 2)
            {
                other.GetComponent<Enemy_Core>().InvokeDamage(playerAttack.attackStats.damageATK2, PlayerObject.transform.position, playerAttack.attackStats.stunTimeATK2);
            }
            else if (playerAttack.currentHitCombo == 3)
            {
                other.GetComponent<Enemy_Core>().InvokeDamage(playerAttack.attackStats.damageATK3, PlayerObject.transform.position, playerAttack.attackStats.stunTimeATK3);
            }
                
        }
    }
}
