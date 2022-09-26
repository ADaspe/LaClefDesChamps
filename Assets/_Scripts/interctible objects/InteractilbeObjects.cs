using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractilbeObjects : MonoBehaviour
{
    [SerializeField] float waitForDeath;
    public  GameObject Vizuals;
    public ParticleSystem[] VFX;
    public ParticleSystem DeathVFX;

    public void BurnObject()
    {
        StartCoroutine(WaitForDeath());
        for (int i = 0; i < VFX.Length; i++)
        {
            VFX[i].gameObject.SetActive(true);
        }
    }
 
    IEnumerator WaitForDeath()     // is it realy clean ?
    {
        yield return new WaitForSeconds(waitForDeath);
        
        Vizuals.SetActive(false);
        DeathVFX.gameObject.SetActive(true);
        if (GetComponent<Looter>() != null) { GetComponent<Looter>().Loot(); }
       
        for (int i = 0; i < VFX.Length; i++)
        {
            VFX[i].gameObject.SetActive(false);
        }
        
        Destroy(gameObject, 4f);
    }
}