using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Settings", menuName = "Scriptable Objects/Attack Settings", order = 1)]
public class AttackSettingsSO : ScriptableObject
{
    [Header("Global Settings")]
    public int GlobalChargeNumber;

    [Header("Attack 1")]
    public int damageATK1;
    public float stunTimeATK1;
    public float MaxInputDelayATK1;

    [Header("Attack 2")]
    public int damageATK2;
    public float stunTimeATK2;
    public float MaxInputDelayATK2;

    [Header("Attack 3 No Element")]
    public int damageATK3;
    public float stunTimeATK3;
    public float MaxInputDelayATK3;

    [Header("Attack 3 Fire")]

    public int damageATK3Fire;
    public float stunTimeATK3Fire;
    public float MaxInputDelayATK3Fire;
    public int MaxChargeFire;

    [Header("Attack 3 Frog")]

    public float frogTimeATK3;
    public float stunTimeATK3Frog;
    public float MaxInputDelayATK3Frog;
    public int MaxChargeFrog;

    [Header("Attack 3 Metal")]

    public float damageReductionATK3Metal;
    public float MaxInputDelayATK3Metal;
    public int MaxChargeMetal;

    [Header("Attack 3 Firefly")]

    public float stunTimeATK3Firefly;
    public float MaxInputDelayATK3Firefly;
    public int MaxChargeFirefly;

}
