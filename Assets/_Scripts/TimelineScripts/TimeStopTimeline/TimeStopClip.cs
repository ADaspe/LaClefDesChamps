using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class TimeStopClip : PlayableAsset, ITimelineClipAsset
{
    public TimeStopBehaviour template = new TimeStopBehaviour();
    public ExposedReference<TimeStopManager> timeManager;
    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<TimeStopBehaviour>.Create(graph, template);
        TimeStopBehaviour clone = playable.GetBehaviour();
        clone.timeManager = timeManager.Resolve(graph.GetResolver());

        return playable;
    }
}
