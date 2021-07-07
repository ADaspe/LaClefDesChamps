using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class DialogClip : PlayableAsset, ITimelineClipAsset
{
    public DialogBehavior template = new DialogBehavior();

    public ExposedReference<DialogManager> dialogManager;

    public ClipCaps clipCaps
    {
        get { return ClipCaps.None; }
    }

    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<DialogBehavior>.Create(graph, template);
        DialogBehavior clone = playable.GetBehaviour();
        clone.dialogManager = dialogManager.Resolve(graph.GetResolver());

        return playable;
    }
}
