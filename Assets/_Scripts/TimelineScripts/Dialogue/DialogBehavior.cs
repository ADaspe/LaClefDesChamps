using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class DialogBehavior : PlayableBehaviour
{
    public string text;
    public string name;
    public float typingSpeed;

    [HideInInspector] public DialogManager dialogManager;

    private PlayableDirector director;
    

    public override void OnGraphStart(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);

    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        dialogManager.SetText(text, name, typingSpeed);
    }

    public override void OnGraphStop(Playable playable)
    {
        dialogManager.ResetText();
    }

}
