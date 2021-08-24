using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Brambles : MonoBehaviour
{
    public Collider detectorColldier;
    public List<GameObject> VFXs;
    public float timeToDelayDestruction;

    private void Start()
    {
        detectorColldier = GetComponent<Collider>();
    }

    public void DestroyBrambles()
    {
        foreach (GameObject VFX in VFXs)
        {
            VFX.SetActive(true);
        }
        Destroy(gameObject, timeToDelayDestruction);
    }
}
