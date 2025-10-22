using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    private void Start()
    {
        Invoke("LoadTitleScene", 1f);

        ManagerObject.instance.soundManager.StopAllAudioClip();


    }

    private void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
