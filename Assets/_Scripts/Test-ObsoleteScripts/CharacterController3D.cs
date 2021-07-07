using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController3D : MonoBehaviour
{
    [SerializeField] private float speed = 0;
    [SerializeField] private float animationBlend = 2f;
    CharacterController playerController;
    Animator playerAnimator;
    private Vector3 lastDirection = new Vector3(0, 0, 0);    

    private void Start()
    {
        playerController = gameObject.GetComponent<CharacterController>();
        playerAnimator = gameObject.GetComponentInChildren<Animator>();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(x, 0, z).normalized;
        if(movement.magnitude > 0.1)
        {
            lastDirection = movement;
            float blendTarget = 1f;
            playerAnimator.SetFloat("Blend", Mathf.Lerp( playerAnimator.GetFloat("Blend"), blendTarget, animationBlend * Time.deltaTime));

        }
        else
        {
            float blendTarget = 0f;
            playerAnimator.SetFloat("Blend", Mathf.Lerp(playerAnimator.GetFloat("Blend"), blendTarget, animationBlend * Time.deltaTime));
        }
        
        Quaternion rotation = Quaternion.LookRotation(lastDirection, Vector3.up);
        transform.rotation = rotation;
        playerController.Move(movement * Time.deltaTime * speed);
    }
   
}
