using UnityEngine;

[CreateAssetMenu(menuName = "Game/DoorKey Data")]
public class DoorKeyDataSO : ScriptableObject
{
    public DoorKeyData[] doorKeyDatas;

    public DoorKeyData GetTriggerDataById(string keyDoorID)
    {
        foreach (var data in doorKeyDatas)
        {
            if (data.keyDoorID == keyDoorID)
                return data;
        }
        return null;
    }

}

[System.Serializable]
public class DoorKeyData
{
    public string keyDoorID;
    public string activeChapter;
    [SerializeField]
    private string doorName = "";
    public string DoorName => doorName;

    [SerializeField]
    private string keyName = "";
    public string KeyName => keyName;
}
