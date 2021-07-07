using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightElement : AbsorbElement
{
    public override void OnAbsorb(PlayerBook book)
    {
        base.OnAbsorb(book);
        Debug.Log("[Light Element - On Absorb] L'élément lumière vient d'être absorbé");
    }

    public override void OnRelease(PlayerBook book)
    {
        base.OnRelease(book);
        Debug.Log("[Light Element - On Release] L'élément lumière vient d'être utilisé");
    }

    public LightElement(Element element) : base(element)
    {

    }
}
