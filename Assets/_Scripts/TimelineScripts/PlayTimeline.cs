using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class PlayTimeline : MonoBehaviour
{

    [SerializeField] private PlayableDirector director = null;

    public void PlayDirector()
    {
        if(director != null)
        director.Play();
    }
}
