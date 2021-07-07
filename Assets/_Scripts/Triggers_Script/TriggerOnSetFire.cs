using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnSetFire : MonoBehaviour
{
    public UnityEvent OnSetFire;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[Trigger On Set Fire] L'objet est en contact avec un collider : " + collision);
        if (collision.gameObject.CompareTag("Fire"))
        {
            OnSetFire?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Trigger On Set Fire] L'objet est en contact avec un collider : " + other);
        if (other.gameObject.CompareTag("Fire"))
        {
            Debug.Log("[Trigger On Set Fire ] L'objet est reconnu comme ayant pris feu");
            OnSetFire?.Invoke();
        }
    }
}
