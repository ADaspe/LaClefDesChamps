using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbsorbElements : MonoBehaviour
{
    public enum Elements  
    {
        empty   = 0,
        fire,
        metal,
        light,
        grab
    }

    private Elements SetElement(Elements currentElement)
    {
        if           (transform.CompareTag("Fire")) currentElement = Elements.fire;
        else if      (transform.CompareTag("Metal")) currentElement = Elements.metal;
        return currentElement;
    }
    public Elements absElements;

    void Update()
    {
        switch (absElements)
        {
            case Elements.fire:
                // fait des trucs la genre ajouter des charges 
                break;
            case Elements.metal:
                break;
            case Elements.light:
                break;
            case Elements.grab:
                break;
            default:
                break;
        }
    }
}
