using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testTriggers : MonoBehaviour
{
    private List<Collider> Targets = new List<Collider>();

    private void OnTriggerEnter(Collider other)
    {
        print("Triggering the collider");

        if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))// && !Targets.Contains(MobMob))
        {
            Targets.Add(other);
            print("MOB MOB IN RANGE + addint it to the list");
            print(Targets.Count);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        print("satying in the fire");
    }
}


