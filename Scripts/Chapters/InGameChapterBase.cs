using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class InGameChapterBase : MonoBehaviour
{
    public TriggerEventManager triggerEvent = new TriggerEventManager();
    public DoorKeyManager doorKey = new DoorKeyManager();
    private GameObject[] inven = new GameObject[30];
    private string[] invencontentname = new string[30];
    private int invencount = 0;

    private void Awake()
    {
        ManagerObject.instance.resourceManager.CacheSceneObjects(); //씬에 있는 오브젝트들을 캐싱
        ManagerObject.instance.eventManager.ThisScenePlayer = GameObject.FindGameObjectWithTag("Player"); // 현재 씬에서의 Player 오브젝트, 씬에서 한번만 실행
        
        ManagerObject.instance.eventManager.GetItemEvent -= GetItem;
        ManagerObject.instance.eventManager.GetItemEvent += GetItem;
        ManagerObject.instance.eventManager.FindInvenEvent -= FindInven;
        ManagerObject.instance.eventManager.FindInvenEvent += FindInven;

    }

    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        triggerEvent.OnStart(SceneManager.GetActiveScene()); //현재씬의 트리거 로드
        doorKey.OnStart(); //현재씬의 문과 열쇠 매칭


    }

    protected void Update()
    {
        triggerEvent.OnUpdate(); //TODO: event Action을 통해 트리거 발동 -> 업데이트할 필요가 있으면 이벤트 구독 후 ?.Invoke()로 사용하도록 변경 고려
    }


    protected virtual void OnDestroy()
    {
        ManagerObject.instance.eventManager.GetItemEvent -= GetItem;
        ManagerObject.instance.eventManager.FindInvenEvent -= FindInven;
        triggerEvent.OnDestroy();

    }

    private void GetItem(GameObject item)
    {
        //아이템 획득 시 자동으로 실행되는 이벤트는 TriggerEventManager에서 델리게이트로 처리 하도록 설정하였음.
        inven[invencount] = item;
        InvenNameSubmit();
        invencount += 1;

        GameObject itemininventoryprefab = new GameObject();
        itemininventoryprefab.transform.SetParent(ManagerObject.instance.eventManager.OnGetContentFolder().transform); //프리팹 소환 시 content 아래에 생성
        itemininventoryprefab.AddComponent<Image>().sprite = item.GetComponent<Image>().sprite;
    }
    private void InvenNameSubmit()
    {
        invencontentname[invencount] = inven[invencount].name;
    }
    private bool FindInven(string name)
    {
        int pos = Array.IndexOf(invencontentname, name);
        if (pos > -1)
        {
            return true;
        }
        else
        {
            return false;
        }

    }


}