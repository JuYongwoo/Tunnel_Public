
public class Chapter3 : InGameChapterBase
{


    protected override void Start()
    {
        base.Start();

        triggerEvent.PlayEvent("Chapter3Start");

        ManagerObject.instance.soundManager.StopAllAudioClip();
    }

}
