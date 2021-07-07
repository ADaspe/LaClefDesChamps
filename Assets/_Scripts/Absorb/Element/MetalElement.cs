using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetalElement : AbsorbElement
{

    public override void OnAbsorb(PlayerBook book)
    {
        base.OnAbsorb(book);
        Debug.Log("[Metal Element - On Absorb] L'élément métal vient d'être absorbé");
    }

    public override void OnRelease(PlayerBook book)
    {
        base.OnRelease(book);

        GameObject metalArrow = GameObject.Instantiate(book.metalPrefab, book.arrowParent.position, book.transform.rotation);
        MetalArrow arrow = metalArrow.GetComponent<MetalArrow>();
        arrow.SetLifeTime(book.arrowLifetime);
        arrow.rb.AddForce(arrow.transform.forward * book.arrowforce, ForceMode.VelocityChange);

        //GameObject.Instantiate(book.firePrefab, book.transform);
        Debug.Log("[Metal Element - On Release] L'élément métal vient d'être relâché");
    }

    public MetalElement(Element element) : base(element)
    {

    }
}
