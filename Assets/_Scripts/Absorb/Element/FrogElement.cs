using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrogElement : AbsorbElement
{
    public override void OnAbsorb(PlayerBook book)
    {
        base.OnAbsorb(book);
        Debug.Log("[Frog Element - On Absorb] L'élément grenouille vient d'être absorbé");
    }

    public override void OnRelease(PlayerBook book)
    {
        base.OnRelease(book);       
        Debug.Log("[Frog Element - On Release] L'élément grenouille vient d'être utilisé");
    }

    public FrogElement(Element element) : base(element)
    {

    }
}
