using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class TriggerEventManager
{


    public string NowMission = "";


    public void OnStart(Scene currentScene)
    {
        ManagerObject.instance.actionManager.getNowMissionText -= getNowMission;
        ManagerObject.instance.actionManager.getNowMissionText += getNowMission;
        mappingAlltriggerEvents(currentScene);
        readyAlltriggerEvents();
    }
    public void OnDestroy()
    {
        ManagerObject.instance.actionManager.getNowMissionText -= getNowMission;
    }


    public void OnUpdate()
    {
        if (ManagerObject.instance.actionManager.ThisScenePlayer == null) return;

        foreach (var TriggerEvent in ManagerObject.instance.resource.triggerEvents.Result.triggerEvents)
        {
            if (!TriggerEvent.isThisChapter) continue;
            if (TriggerEvent.istriggered && TriggerEvent.EventFollower != "")
            {
                ManagerObject.instance.SpawnRegistry[TriggerEvent.EventFollower].GetComponent<NavMeshAgent>().destination = ManagerObject.instance.actionManager.ThisScenePlayer.transform.position;
            }

        }

    }


    public void mappingAlltriggerEvents(Scene currentScene)
    {
        foreach (var trigger in ManagerObject.instance.resource.triggerEvents.Result.triggerEvents)
        {
            if (trigger.activeSceneName == currentScene.name)
            {
                trigger.isThisChapter = true;
            }
            else if (trigger.activeSceneName != currentScene.name)
            {
                trigger.isThisChapter = false;
            }
            trigger.istriggered = false; //다시 실행안했던 것으로 돌려놓기

        }


    }

    public void readyAlltriggerEvents()
    {


        foreach (var trigger in ManagerObject.instance.resource.triggerEvents.Result.triggerEvents)
        {
            if (trigger.isThisChapter)
            {
                if (trigger.IsColliderTriggerActive)
                {
                    CreateCollisionTrigger(trigger);
                }
                else if (trigger.IsItemTriggerActive)
                {
                    CreateItemTrigger(trigger);
                }

            }
        }
    }

    public void followermove()
    {

    }


    void CreateCollisionTrigger(TriggerEvent trigger)
    {
        GameObject go = new GameObject($"Trigger_{trigger.triggerID}");
        go.transform.position = trigger.TriggerPosition;

        var col = go.AddComponent<BoxCollider>();
        col.isTrigger = true;
        col.size = Vector3.one * trigger.TriggerDistance;

        var proxy = go.AddComponent<GameObjectForCollisionTrigger>();
        proxy.TriggerEnterAction = () =>
        {
            if (!trigger.istriggered)
            {
                PlayEvent(trigger.triggerID);
            }
            trigger.istriggered = true;
        };
    }

    void CreateItemTrigger(TriggerEvent trigger)
    {
        Item go = GameObject.Find(trigger.TriggerGetItem).GetComponent<Item>();
        go.DisableAction = () =>
        {
            if (!trigger.istriggered)
            {
                PlayEvent(trigger.triggerID);
            }
            trigger.istriggered = true;
        };

    }


    private class GameObjectForCollisionTrigger : MonoBehaviour
    {
        public Action TriggerEnterAction;

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                TriggerEnterAction?.Invoke();
            }
        }
    }

    public string getNowMission()
    {
        return NowMission;
    }

    public void PlayEvent(string eventname)
    {
        if (ManagerObject.instance.resource.triggerEvents.Result.GetTriggerDataById(eventname) != null)
        {
            TriggerEvent te = ManagerObject.instance.resource.triggerEvents.Result.GetTriggerDataById(eventname);
            if (ManagerObject.instance.resource.triggerEvents.Result.GetTriggerDataById(eventname).IsDescription)
            {
                NowMission = te.EventDescription;

            }
            if (te.IsSpeech)
            {
                ManagerObject.instance.actionManager.OnSpeech((List<string>)te.EventSpeeches);
            }

            if (te.IsSceneChange) 
            {
                SceneManager.LoadScene(te.EventSceneChange);
            }
            if (te.IsSound)
            {
                ManagerObject.instance.sound.PlayAudioClip(ManagerObject.instance.resource.soundsmap[te.Sound].Result, 0.3f, true);
            }
            if (te.IsSave)
            {
                PlayerPrefs.SetInt("chap", te.Save);

            }
            if (te.IsFadeIn)
            {
                ManagerObject.instance.actionManager.OnFadeIn();

            }
            if (te.IsFadeOut)
            {
                ManagerObject.instance.actionManager.OnFadeOut();
            }

            GameObject go = null;

            if (te.IsSpawnObject)
            {
                //GameObject go = GameObject.Instantiate(Resources.Load<GameObject>($"Prefabs/" + triggereventmap[eventname].SpawnObjectName));
                go = ManagerObject.instance.resource.Instantiate(te.SpawnPrefabPathName, te.SpawnNaming);
                go.transform.position = te.SpawnObjectPosition;
            }
            if (te.IsFollow)
            {
                if (te.SpawnPrefabPathName == te.EventFollower)
                {
                    Util.AddOrGetComponent<Following>(go);
                }
                else
                {
                    ManagerObject.instance.SpawnRegistry.TryGetValue(te.EventFollower, out GameObject go2);
                    Util.AddOrGetComponent<Following>(go2);
                }
            }
            if (te.IsPatrol)
            {
                if (te.SpawnPrefabPathName == te.PatrolObjectName)
                {
                    Patrolling Patroll = Util.AddOrGetComponent<Patrolling>(go);
                    Patroll.patrolpositions = (List<Vector3>)te.EventPatrolPositions;
                }
                else
                {
                    GameObject go2 = GameObject.Find(te.PatrolObjectName);
                    Patrolling Patroll = Util.AddOrGetComponent<Patrolling>(go2);
                    Patroll.patrolpositions = (List<Vector3>)te.EventPatrolPositions;

                }
            }

        }
        else
        {
            Debug.LogWarning($"No event found for ID: {eventname}");
        }
    }


}
