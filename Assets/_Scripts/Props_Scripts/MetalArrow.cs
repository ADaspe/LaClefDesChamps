using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalArrow : MonoBehaviour
{
    public Rigidbody rb;

    public void SetLifeTime(float lifetime)
    { 
        Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //Decrease progressif de l'alpha
    }

}
