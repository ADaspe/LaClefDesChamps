using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbManager : MonoBehaviour
{
    public static AbsorbManager instance;

    [SerializeField] Renderer Eye;
    [SerializeField] PlayerVFX playerVFX;
    [SerializeField] private Collider absrobCollider;

    //[HideInInspector] 
    public int powerChargesAmount;
    //[HideInInspector] 
    public Elements powerType;
    public enum Elements     // ya surement doublon sur cet enum
    {
        empty = 0,
        fire,
        metal,
        light,
        grab
    }

    [Header("              Absorb Parameters ")]
    [SerializeField] int FireChargeAmount;
    [SerializeField] int MetalChargeAmount;
    [SerializeField] int LightChargeAmount;

    private void Start()
    {
        instance = this;
    }   // Instanciate the static variable of the AbsorbManager

    public void SetPower(int chargeAmount, Elements chargeType)
    {
        powerChargesAmount = chargeAmount;
        powerType = chargeType;
    }
    public void EnableCollider()
    {
        absrobCollider.enabled = true;
        StartCoroutine(ResteCollider());
    }

    private void OnTriggerEnter(Collider other)     // Enable collider when the Fx is played // prevent from charging more then a given amount !!
    {
        // pas trouver moyen de faire autrement qu'avec un gors tableau de if else
        if (other.CompareTag("Fire"))   // filtre si le tag  n'est pas le bon   
        {
            SetPower(FireChargeAmount, Elements.fire);
            Eye.material.SetColor("_EyeColor", Color.red);
            Eye.material.SetFloat("_oui", 1);
            UIManager.instance.InitialChargeAmount();
        }

        else if (other.CompareTag("Metal"))
        {
            Eye.material.SetFloat("_oui", 1);
            Eye.material.SetColor("_EyeColor", Color.blue);
            SetPower(FireChargeAmount, Elements.metal);
            UIManager.instance.InitialChargeAmount();
        }

        else if (other.CompareTag("Light"))
        {
            Eye.material.SetColor("_EyeColor", Color.yellow);
            Eye.material.SetFloat("_oui", 1);
            SetPower(FireChargeAmount, Elements.light);
            UIManager.instance.InitialChargeAmount();
        }

        UIManager.instance.SwapPowerIcone();
        UIManager.instance.compteurCharges = 0;
    }
    IEnumerator ResteCollider()     // is it realy clean ?
    {
        yield return new WaitForSeconds(.01f);
        absrobCollider.enabled = false;
    }
}