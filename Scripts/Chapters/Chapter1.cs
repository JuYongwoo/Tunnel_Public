
public class Chapter1 : InGameChapterBase
{


    protected override void Start()
    {
        base.Start();
        triggerEvent.PlayEvent("Chapter1_StartEvent");

        ManagerObject.instance.soundManager.StopAllAudioClip();

    }



}