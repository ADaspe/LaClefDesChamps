using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeStopBehaviour : PlayableBehaviour
{
    [HideInInspector] public TimeStopManager timeManager;
    private PlayableDirector director;
    private bool clipPlayed = false;

    public override void OnGraphStart(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
        
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        clipPlayed = true;
        timeManager.ShowPressKeyPannel(true); 
        timeManager.StopTime(director);

    }
    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (clipPlayed) timeManager.ShowPressKeyPannel(false);
    }
}
