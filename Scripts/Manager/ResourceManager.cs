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

    public void cacheSceneObjects() //씬에 시작할 때부터 배치되어 있는 게임 오브젝트들 또한 스폰오프젝트 맵에 등록
    {
        ManagerObject.instance.SpawnRegistry = new Dictionary<string, GameObject>(); //초기화
        
        foreach (var followTarget in GameObject.FindObjectsOfType<MonoBehaviour>(true)) //Find이나, 씬마다 한번만 실행
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
        ManagerObject.instance.SpawnRegistry.Add(instance.name, instance); //소환한 오브젝트들을 상대로 탐색할 일이 많기 때문에 소환 시 맵에 등록
        return instance;
    }

}
