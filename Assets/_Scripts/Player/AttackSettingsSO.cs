using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Settings", menuName = "Scriptable Objects/Attack Settings", order = 1)]
public class AttackSettingsSO : ScriptableObject
{
    [Header("Global Settings")]
    public int GlobalChargeNumber;
    public int ReleaseCost;
    public int Attack3Cost;

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

    public int damagePerTickATK3Fire;
    public float dotTimeATK3Fire;
    public float tickPerSecondATK3Fire;
    public float maxDistanceDetectionATK3Fire;
    public float maxAngleDetectionATK3Fire;
    public float MaxInputDelayATK3Fire;
    public int AdditionnalFireCharge;

    [Header("Attack 3 Frog")]

    public float frogTimeATK3;
    public float maxDistanceDetectionATK3Frog;
    public float maxAngleDetectionATK3Frog;
    public float grabSpeed;
    public float grabMinDistance;
    public float MaxInputDelayATK3Frog;
    public int AdditionnalFrogCharge;

    [Header("Attack 3 Metal")]

    public float damageReductionATK3Metal;
    public float MaxInputDelayATK3Metal;
    public int AdditionnalMetalCharge;

    [Header("Attack 3 Firefly")]

    public float stunTimeATK3Firefly;
    public float maxDistanceDetectionATK3Firefly;
    public float MaxInputDelayATK3Firefly;
    public int AdditionnalFireflyCharge;

}
