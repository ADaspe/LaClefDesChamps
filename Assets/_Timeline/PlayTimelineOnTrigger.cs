using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;
using Player;
using System;

public class PlayTimelineOnTrigger : MonoBehaviour
{
    [SerializeField] private PlayableDirector transitionTimeline = null;
    [SerializeField] private PlayerController player = null;

    private void OnEnable()
    {
        transitionTimeline.stopped += ResumePlaying;
    }

    private void ResumePlaying(PlayableDirector director)
    {
        Debug.Log("Timeline completed");
        player.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger enter");
        player.enabled = false;
        transitionTimeline.Play();
    }

    private void OnDisable()
    {
        transitionTimeline.stopped -= ResumePlaying;
    }
}
