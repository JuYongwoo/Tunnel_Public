using UnityEngine;

public class TitleScene : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;



    }
    private void Start()
    {
        ManagerObject.instance.sound.StopAllAudioClip();

    }
}