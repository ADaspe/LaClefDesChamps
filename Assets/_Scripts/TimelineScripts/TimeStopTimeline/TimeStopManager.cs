using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TimeStopManager : MonoBehaviour
{
    bool timelineStopped = false;
    private PlayableDirector currentDirector;
    [SerializeField] private DialogUI dialogUI = null;

    public void StopTime(PlayableDirector director)
    {
        director.playableGraph.GetRootPlayable(0).SetSpeed(0d);
        currentDirector = director;
        timelineStopped = true;
    }

    public void ResumeTime()
    {
        currentDirector.playableGraph.GetRootPlayable(0).SetSpeed(1d);
    }

    private void Update()
    {
        if(timelineStopped)
        {
            if (Input.anyKeyDown)
            {
                ResumeTime();
                timelineStopped = false;
            }
        }
    }

    public void ShowPressKeyPannel(bool show)
    {
        dialogUI.ToggleSpacebarPanel(show);
    }


}
