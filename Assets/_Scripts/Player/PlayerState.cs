using UnityEngine;
using Player;

public abstract class PlayerState
{
    public abstract void EnterState(PlayerController player);

    public abstract void UpdateState(PlayerController player);

    public abstract void FixedUpdate(PlayerController player);

    public abstract void OnTriggerEnter(PlayerController player, Collider collider);

    public abstract void OnTriggerStay(PlayerController player, Collider collider);

    public abstract void OnTriggerExit(PlayerController player, Collider collider);
}

