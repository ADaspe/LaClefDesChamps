using Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXD_Player_Attack3_Firefly : PlayerState
{
    public float stateTime;
    public override void EnterState(PlayerController player)
    {
        player.currentHitCombo = 3;
        stateTime = Time.time;
    }

    public override void FixedUpdateState(PlayerController player)
    {
    }

    public override void OnTriggerEnter(PlayerController player, Collider collider)
    {
    }

    public override void OnTriggerExit(PlayerController player, Collider collider)
    {
        
    }

    public override void OnTriggerStay(PlayerController player, Collider collider)
    {
    }

    public override void UpdateState(PlayerController player)
    {
        
    }
}
