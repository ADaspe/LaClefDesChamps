using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stele : MonoBehaviour
{
    public bool activated = false;
    public GameObject particleFx;

    public MeshRenderer matToChange;
    public Material changeMat;

    public void ActivateStele()
    {
        Material[] newMat = matToChange.materials;
        newMat[1] = changeMat;

        matToChange.materials = newMat;
        particleFx.SetActive(false);
        activated = true;
    }
}
