using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Lever : MonoBehaviour
{
    [SerializeField] UnityEvent onActivate = null;
    public void ActivateLever()
    {
        //Jouer l'anim d'activation du Levier

        //Jouer le son d'activation du Levier
        onActivate?.Invoke();
    }
}
