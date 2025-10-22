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
        ManagerObject.instance.resourceManager.CacheSceneObjects(); //���� �ִ� ������Ʈ���� ĳ��
        ManagerObject.instance.eventManager.ThisScenePlayer = GameObject.FindGameObjectWithTag("Player"); // ���� �������� Player ������Ʈ, ������ �ѹ��� ����
        
        ManagerObject.instance.eventManager.GetItemEvent -= GetItem;
        ManagerObject.instance.eventManager.GetItemEvent += GetItem;
        ManagerObject.instance.eventManager.FindInvenEvent -= FindInven;
        ManagerObject.instance.eventManager.FindInvenEvent += FindInven;

    }

    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        triggerEvent.OnStart(SceneManager.GetActiveScene()); //������� Ʈ���� �ε�
        doorKey.OnStart(); //������� ���� ���� ��Ī


    }

    protected void Update()
    {
        triggerEvent.OnUpdate(); //TODO: event Action�� ���� Ʈ���� �ߵ� -> ������Ʈ�� �ʿ䰡 ������ �̺�Ʈ ���� �� ?.Invoke()�� ����ϵ��� ���� ���
    }


    protected virtual void OnDestroy()
    {
        ManagerObject.instance.eventManager.GetItemEvent -= GetItem;
        ManagerObject.instance.eventManager.FindInvenEvent -= FindInven;
        triggerEvent.OnDestroy();

    }

    private void GetItem(GameObject item)
    {
        //������ ȹ�� �� �ڵ����� ����Ǵ� �̺�Ʈ�� TriggerEventManager���� ��������Ʈ�� ó�� �ϵ��� �����Ͽ���.
        inven[invencount] = item;
        InvenNameSubmit();
        invencount += 1;

        GameObject itemininventoryprefab = new GameObject();
        itemininventoryprefab.transform.SetParent(ManagerObject.instance.eventManager.OnGetContentFolder().transform); //������ ��ȯ �� content �Ʒ��� ����
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