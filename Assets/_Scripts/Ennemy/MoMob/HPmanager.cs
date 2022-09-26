using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPmanager : MonoBehaviour
{
    private MobMobState mobMobState;

    [SerializeField] int MaxHealt = 10;
    //[HideInInspector]
    public float currenthealth;
    public int compteur = 0;

    private void Start()
    {
        mobMobState = GetComponent<MobMobState>();
        currenthealth = MaxHealt;
        //SetMaxhealth();
    }

    public void FireDOT(int numberOfTick, int tickDamageAmount)
    {
        if( (compteur + 1) < numberOfTick )
        {
            StartCoroutine(oui(numberOfTick, tickDamageAmount));
        }
    }

    public void TakeDamage(int damage)
    {
        currenthealth = currenthealth - damage;
        Die();          
    }

    private void Die()
    {
        if (currenthealth <= 0)
        {
            mobMobState.ChangeState(MobMobState.MobStates.dead);    // dosen't work for some shady reasons 
        }
    }
    public void SetMaxhealth()
    {
        //slider.maxValue = MaxHealt;
        //slider.value = currenthealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) TakeDamage(10);    // DEBUG
    }

    IEnumerator oui(int numberOfTick, int tickDamageAmount)
    {
        yield return new WaitForSeconds(1);
        StopAllCoroutines();
        TakeDamage(tickDamageAmount);
        FireDOT( numberOfTick, tickDamageAmount);
        compteur++;
    }
}