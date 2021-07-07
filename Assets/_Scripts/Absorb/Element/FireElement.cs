using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireElement : AbsorbElement
{

    public override void OnAbsorb(PlayerBook book)
    {
        base.OnAbsorb(book);
        Debug.Log("[Fire Element - On Absorb] L'élément feu vient d'être absorbé");
    }

    public override void OnRelease(PlayerBook book)
    {
        base.OnRelease(book);
        GameObject.Instantiate(book.firePrefab, book.transform);
        Debug.Log("[Fire Element - On Release] L'élément feu vient d'être relâché");
    }

    public FireElement(Element element) : base(element)
    {
        
    }
}
