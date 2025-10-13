using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2 : InGameChapterBase
{

    protected override void Start()
    {
        base.Start();
        triggerEvent.PlayEvent("Chapter2Start");

        ManagerObject.instance.sound.StopAllAudioClip();
    }
}