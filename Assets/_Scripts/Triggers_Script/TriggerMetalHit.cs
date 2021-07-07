using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerMetalHit : MonoBehaviour
{
    public UnityEvent onMetalHit;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("[Trigger On hit Metal] La flèche de métal est rentré en contact avec un collider : " + collision);
        if (collision.gameObject.CompareTag("Metal"))
        {
            Debug.Log("[Trigger On hit Metal] La flèche de métal est rentré en contact avec un collider : ");
            onMetalHit?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[Trigger On hit Metal] La flèche de métal est rentré en contact avec un collider : " + other);
        if (other.gameObject.CompareTag("Metal"))
        {
            Debug.Log("[Trigger On hit Metal] La flèche de métal est rentré en contact avec un collider : ");
            onMetalHit?.Invoke();
        }
    }
}
