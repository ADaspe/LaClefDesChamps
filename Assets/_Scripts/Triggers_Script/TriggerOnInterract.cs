using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerOnInterract : MonoBehaviour
{
    //[SerializeField] private Collider trigger;
    [SerializeField] private UnityEvent onInterract = null;
    private bool playerNear = false;

    void Update()
    {
        if(playerNear)
        {
            if(Input.GetButtonDown("Fire2"))
            {
                Debug.Log("[On Interract] : Déclenchement de l'évènement lié");
                onInterract?.Invoke();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerNear = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
        }
    }
}
