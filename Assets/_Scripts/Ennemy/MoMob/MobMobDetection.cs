using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMobDetection : MonoBehaviour
{
    [SerializeField] float mobPatience;
   
    [SerializeField] MobMobState mobMobState;
    [SerializeField] UCharacterController uCharacterController;
    [SerializeField] float sneakMaxSpeed;   // don't work

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PLayer")) 
        {
            StopAllCoroutines();
            mobMobState.playerDetected = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("PLayer")) { StartCoroutine(ResetPatience()); }
    }
    private void Update()
    {
        if(uCharacterController.characterController.velocity.magnitude > sneakMaxSpeed && mobMobState.inChaseRange())   // don't work without controller
        {
            mobMobState.playerDetected = true;
        }
    }
    IEnumerator ResetPatience()
    {
        yield return new WaitForSeconds(mobPatience);
        mobMobState.playerDetected = false;
    }
}