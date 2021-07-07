using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShojinState
{
    public abstract void EnterState(Shojin shojin);

    public abstract void Update(Shojin shojin);
}
