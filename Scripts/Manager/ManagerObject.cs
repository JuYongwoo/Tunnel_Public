using System.Collections.Generic;
using UnityEngine;

public class ManagerObject : MonoBehaviour
{
    public static ManagerObject instance;


    public SoundManager sound = new SoundManager();
    public ResourceManager resource = new ResourceManager();
    public ActionManager actionManager = new ActionManager();
    public InputManager inputManager = new InputManager();

    [HideInInspector]
    public Dictionary<string, GameObject> SpawnRegistry;


    public void Awake()
    {
        singletone();

        resource.preLoad();

    }

    public void Update()
    {
        inputManager.OnUpdate();
    }

    private void singletone()
    {
        if (instance != null && instance != gameObject)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
