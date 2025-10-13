using UnityEngine;


public class DoorKeyManager
{


    public void OnStart()
    {
        Door[] doors = GameObject.FindObjectsOfType<Door>(); //���� ���� �ִ� ���鿡 ����
        foreach (var doorkey in ManagerObject.instance.resource.doorKeyMap.Result.doorKeyDatas)
        {
            foreach (Door door in doors)
            {
                if (doorkey.DoorName == door.name) //�� �̸��� ������
                {
                    door.KeyName = doorkey.KeyName; //���� �̸��� ������
                }
            }
        }
    }

}
