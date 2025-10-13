using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Game/Event Data")]
public class TriggerEventSO : ScriptableObject
{
    public TriggerEvent[] triggerEvents;

    public TriggerEvent GetTriggerDataById(string triggerID)
    {
        foreach (var data in triggerEvents)
        {
            if (data.triggerID == triggerID)
                return data;
        }
        return null;
    }

    public List<TriggerEvent> GetTriggerDatasBySceneName(string activeSceneName) //씬에 해당하는 트리거이벤트들 반환
    {
        List<TriggerEvent> returns = new List<TriggerEvent>();
        foreach (var data in triggerEvents)
        {
            if (data.activeSceneName == activeSceneName) returns.Add(data);
        }
        return returns;
    }

}

[System.Serializable]
public class TriggerEvent
{
    public string triggerID;
    public string activeSceneName;
    [NonSerialized] public bool istriggered = false; //Manager에서 수정 가능해야하므로 public
    [NonSerialized] public bool isThisChapter = false; //Manager에서 수정 가능해야하므로 public

    [SerializeField] private bool isColliderTriggerActive;
    public bool IsColliderTriggerActive => isColliderTriggerActive;

    [SerializeField] private Vector3 triggerPosition;
    public Vector3 TriggerPosition => triggerPosition;

    [SerializeField] private float triggerdistance;
    public float TriggerDistance => triggerdistance;

    [SerializeField] private bool isItemTriggerActive;
    public bool IsItemTriggerActive => isItemTriggerActive;

    [SerializeField] private string tiggerGetItem;
    public string TriggerGetItem => tiggerGetItem;

    [SerializeField] private bool isDescription;
    public bool IsDescription => isDescription;

    [SerializeField] private string eventdescription;
    public string EventDescription => eventdescription;

    [SerializeField] private bool isSpeech;
    public bool IsSpeech => isSpeech;

    [SerializeField] private List<string> eventSpeeches = new List<string>();
    public IReadOnlyList<string> EventSpeeches => eventSpeeches;

    [SerializeField] private bool isSceneChange;
    public bool IsSceneChange => isSceneChange;

    [SerializeField] private string eventscenechange = "";
    public string EventSceneChange => eventscenechange;

    [SerializeField] private bool isSound;
    public bool IsSound => isSound;

    [SerializeField] private SoundsEnum sound;
    public SoundsEnum Sound => sound;

    [SerializeField] private bool isSave;
    public bool IsSave => isSave;

    [SerializeField] private int save = -1;
    public int Save => save;

    [SerializeField] private bool isFadeOut = false;
    public bool IsFadeOut => isFadeOut;

    [SerializeField] private bool isFadeIn = false;
    public bool IsFadeIn => isFadeIn;

    [SerializeField] private bool isSpawnObject = false;
    public bool IsSpawnObject => isSpawnObject;

    [SerializeField] private string spawnPrefabPathName = "";
    public string SpawnPrefabPathName => spawnPrefabPathName;

    [SerializeField] private string spawnNaming = "";
    public string SpawnNaming => spawnNaming;

    [SerializeField] private Vector3 spawnObjectPosition;
    public Vector3 SpawnObjectPosition => spawnObjectPosition;

    [SerializeField] private bool isFollow;
    public bool IsFollow => isFollow;

    [SerializeField] private string eventfollower = "";
    public string EventFollower => eventfollower;

    [SerializeField] private bool isPatrol;
    public bool IsPatrol => isPatrol;

    [SerializeField] private string patrolObjectName = "";
    public string PatrolObjectName => patrolObjectName;

    [SerializeField] private List<Vector3> eventPatrolPositions = new List<Vector3>();
    public IReadOnlyList<Vector3> EventPatrolPositions => eventPatrolPositions;
}