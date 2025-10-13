using System.Collections.Generic;
using UnityEngine;

public class InputManager
{
    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ManagerObject.instance.actionManager.OnInGameTabKeyAction();

        }

        if (Input.GetKeyDown(KeyCode.Q)) {
            ManagerObject.instance.actionManager.OnInGameQKeyAction();


        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ManagerObject.instance.actionManager.OnInGameESCKeyAction();




        }
    }


}