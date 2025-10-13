using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter3 : InGameChapterBase
{


    protected override void Start()
    {
        base.Start();

        triggerEvent.PlayEvent("Chapter3Start");

        ManagerObject.instance.sound.StopAllAudioClip();
    }

}
