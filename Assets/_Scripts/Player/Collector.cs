using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collector : MonoBehaviour
{
    private int _pepinAmount;
    [SerializeField] GameObject collectVFX;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Collectible"))
        {
            other.gameObject.SetActive(false);
         
            Object.Instantiate(collectVFX, other.transform.position, collectVFX.transform.rotation);
            
            if (other.name == "LCR_Pepin_prefab")
            {
                _pepinAmount++;
                UIManager.instance.DisplayPepinScore(_pepinAmount);
            }
        }
    }
}
