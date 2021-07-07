using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;

public class DisablePlayer_Relayer : MonoBehaviour
{
    private PlayerController player;

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void DisablePlayer()
    {
        Debug.Log("Je passe ici");
        player.enabled = false;
    }

    public void EnablePlayer()
    {
        Debug.Log("Je passe ici");
        player.enabled = true;
    }
}
