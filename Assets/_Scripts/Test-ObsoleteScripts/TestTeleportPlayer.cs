using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player;
using System;

public class TestTeleportPlayer : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject playerModel;
    CharacterController playerController;
    GameObject respawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<CharacterController>();
        respawnPoint = GameObject.FindWithTag("Respawn");
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Teleport();
        }

    }

    private void Teleport()
    {
        playerController.enabled = false;
        player.transform.position = respawnPoint.transform.position;// + (playerModel.transform.position - player.transform.position);
        playerController.enabled = true;
    }
}
