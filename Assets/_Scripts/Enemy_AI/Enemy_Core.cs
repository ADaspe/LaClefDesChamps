using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy_Core : MonoBehaviour
{
    public event Action<int, Vector3> onDamage;
    //public event Action onDeath;


    public void InvokeDamage(int damageAmount, Vector3 damageSource)
    {
        onDamage?.Invoke(damageAmount, damageSource);
    }

}
