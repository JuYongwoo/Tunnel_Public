using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public enum ItemType
{
    PictureBook,
    Textbook,
    Key,
    Door,
    MovingWall
}
public enum SoundsEnum
{
    doorsound,
    papersound,
    stepsound,
    BGM
}

public class ResourceManager
{
    public AsyncOperationHandle<DoorKeyDataSO> doorKeyMap;
    public AsyncOperationHandle<TriggerEventSO> triggerEvents;
    public Dictionary<SoundsEnum, AsyncOperationHandle<AudioClip>> soundsmap = new Dictionary<SoundsEnum, AsyncOperationHandle<AudioClip>>();


    public void preLoad()
    {
        doorKeyMap = Util.AsyncLoad<DoorKeyDataSO>("DoorKeys");
        triggerEvents = Util.AsyncLoad<TriggerEventSO>("TriggerEvents");
        soundsmap = Util.LoadDictWithEnum<SoundsEnum, AudioClip>();


    }

    public void cacheSceneObjects() //���� ������ ������ ��ġ�Ǿ� �ִ� ���� ������Ʈ�� ���� ����������Ʈ �ʿ� ���
    {
        ManagerObject.instance.SpawnRegistry = new Dictionary<string, GameObject>(); //�ʱ�ȭ
        
        foreach (var followTarget in GameObject.FindObjectsOfType<MonoBehaviour>(true)) //Find�̳�, ������ �ѹ��� ����
        {
            if (!ManagerObject.instance.SpawnRegistry.ContainsKey(followTarget.gameObject.name))
            {
                ManagerObject.instance.SpawnRegistry.Add(followTarget.gameObject.name, followTarget.gameObject);
            }
        }
    }

    public GameObject Instantiate(string PrimaryKey, string objectName)
    {
        GameObject prefab = Addressables.LoadAssetAsync<GameObject>(PrimaryKey).WaitForCompletion();
        GameObject instance = GameObject.Instantiate(prefab);
        instance.name = objectName;
        ManagerObject.instance.SpawnRegistry.Add(instance.name, instance); //��ȯ�� ������Ʈ���� ���� Ž���� ���� ���� ������ ��ȯ �� �ʿ� ���
        return instance;
    }

}
