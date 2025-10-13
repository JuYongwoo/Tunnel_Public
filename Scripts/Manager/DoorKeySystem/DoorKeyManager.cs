using UnityEngine;


public class DoorKeyManager
{


    public void OnStart()
    {
        Door[] doors = GameObject.FindObjectsOfType<Door>(); //현재 씬에 있는 문들에 적용
        foreach (var doorkey in ManagerObject.instance.resource.doorKeyMap.Result.doorKeyDatas)
        {
            foreach (Door door in doors)
            {
                if (doorkey.DoorName == door.name) //문 이름이 같으면
                {
                    door.KeyName = doorkey.KeyName; //열쇠 이름도 같도록
                }
            }
        }
    }

}
