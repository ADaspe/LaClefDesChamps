using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Light, Fire, Frog, Metal, None }
public abstract class  AbsorbElement
{
    public Element _element;
    protected float _exitTime;

    public void SetExitTime(float exitTime)
    {
        _exitTime = exitTime;
    }

    public float GetExitTime()
    {
        return _exitTime;
    }

    public virtual void OnAbsorb(PlayerBook book)
    {
        Debug.Log("[Absorb Element - On Absorb] Je viens de la classe de base");
    }

    public virtual void OnRelease(PlayerBook book)
    {
        Debug.Log("[Absorb Element - On Release] Je viens de la classe de base");
    }

    protected AbsorbElement(Element element)
    {
        _element = element;
    }
}
