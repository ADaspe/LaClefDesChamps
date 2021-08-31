using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Player_Attack3_Metal : PlayerState
{
    public override void EnterState(PlayerController player)
    {
        player.currentHitCombo = 3;
    }

    public override void FixedUpdateState(PlayerController player)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
        throw new System.NotImplementedException();
    }

    public override void UpdateState(PlayerController player)
    {
        throw new System.NotImplementedException();
    }
}
