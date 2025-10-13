using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter1 : InGameChapterBase
{


    protected override void Start()
    {
        base.Start();
        triggerEvent.PlayEvent("Chapter1_StartEvent");

        ManagerObject.instance.sound.StopAllAudioClip();

    }



}