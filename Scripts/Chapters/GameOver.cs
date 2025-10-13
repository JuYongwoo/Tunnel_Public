using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    void Start()
    {
        Invoke("LoadTitleScene", 1f);

        ManagerObject.instance.sound.StopAllAudioClip();


    }

    void LoadTitleScene()
    {
        SceneManager.LoadScene("Title");
    }
}
