using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    // rien de nouveeau a part INput basic attak + abs
    private Vector3 movedirection;

    private float moveInputX;
    private float moveInputZ;

    //  MOVE INPUTS
    private float GetInputX() => moveInputX = Input.GetAxis("Horizontal");
    private float GetInputZ() => moveInputZ = Input.GetAxis("Vertical");
    public Vector3 MoveInputs() => movedirection = new Vector3(GetInputX(), 0, GetInputZ());

    //  ACTION INPUTS
    public bool AbsorbInput() =>        Input.GetKeyDown(KeyCode.E);
    public bool HealInput() =>          Input.GetKeyDown(KeyCode.M);
    
    //  ATTACKS
    public bool BasicAttackInput() =>   Input.GetKeyDown(KeyCode.R);
    public bool SpecialAttackInput() => Input.GetKeyDown(KeyCode.A);
}