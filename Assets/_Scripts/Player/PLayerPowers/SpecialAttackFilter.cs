using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAttackFilter : MonoBehaviour
{
    private InputReader Inputreader;
    private PlayerState playerState;
    private PlayerVFX playerVFX;

    // Referencing Diferents Special Attack scripts
    private Fire_big Fire_big;
    private Metal_big Metal_big;
    private Light_big Light_big;

    [HideInInspector] public float specialAttackTime;

    public Animator Panimator;

    void Start()
    {
        Inputreader = GetComponentInParent<InputReader>();
        playerState = GetComponentInParent<PlayerState>();
        playerVFX = GetComponentInChildren<PlayerVFX>();

        // Special Power Infos
        Fire_big = GetComponent<Fire_big>();
        Metal_big = GetComponent<Metal_big>();
        Light_big = GetComponent<Light_big>();
    }

    // comprend pas bien ce que fait ce truc /!\
    public void SetAttackPower( PlayerVFX.PowerType nexttype)
    {
        SetVFX(nexttype);
    }

    public void SetVFX(PlayerVFX.PowerType nexttype)        // may be useless 
    {
        playerVFX.powerType = nexttype;         // visuel
    }   // move this to playerVFX ?? 
}
