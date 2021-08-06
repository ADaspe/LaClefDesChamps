using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogAttack : MonoBehaviour
{

    public GameObject target;

    public void SwallowTarget()
    {
        //Romnom some mobmob
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
        {
            target = other.gameObject;

        }
        SwallowTarget();
    }
}
