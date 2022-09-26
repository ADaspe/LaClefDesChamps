using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCharacterController : MonoBehaviour
{
    private InputReader InputReader;
    [SerializeField] PlayerState playerState;
    [SerializeField] CharacterAnimations characterAnimations;
    private SpecialAttackFilter SpecialAttackFilter;

    public CharacterController characterController;

    public float playerVelocity;
    [SerializeField] private float speed;
    [SerializeField] private float gravityAmount;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        SpecialAttackFilter = GetComponentInChildren<SpecialAttackFilter>();
        InputReader = GetComponent<InputReader>();
    }
    // fix perso + rapide en diagonal avec vector.normalized
                                                              // okay c zarbi                                                          // normaliser uniquement si la valuer est basse     
                                                              // return un point sur le cercle 
    public void CharacterMovements() => characterController.Move(new Vector3(InputReader.MoveInputs().x, gravityAmount, InputReader.MoveInputs().z).normalized * speed * Time.deltaTime);
    public void Characterorientation()
    {
        Vector3 CharacterOrientation;
        CharacterOrientation = InputReader.MoveInputs().normalized;
        transform.LookAt(CharacterOrientation + transform.position);
    }
    
    private void Update()
    {
        playerVelocity = characterController.velocity.magnitude;
        
        if (playerState.currentState != PlayerState.PlayerStates.moving) return;
        CharacterMovements();
        Characterorientation();
        characterAnimations.PlayAnimation();
    }
}