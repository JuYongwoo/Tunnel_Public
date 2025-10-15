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

    private GameObject InCodePrefab;


    protected virtual void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ManagerObject.instance.resourceManager.CacheSceneObjects(); //���� �ִ� ������Ʈ���� ĳ��

        ManagerObject.instance.actionManager.ThisScenePlayer = GameObject.FindGameObjectWithTag("Player"); // ���� �������� Player ������Ʈ, ������ �ѹ��� ����


        triggerEvent.OnStart(SceneManager.GetActiveScene()); //������� Ʈ���� �ε�
        doorKey.OnStart(); //������� ���� ���� ��Ī


        ManagerObject.instance.actionManager.GetItemEvent -= GetItem;
        ManagerObject.instance.actionManager.GetItemEvent += GetItem;
        ManagerObject.instance.actionManager.FindInvenEvent -= FindInven;
        ManagerObject.instance.actionManager.FindInvenEvent += FindInven;

    }

    protected void Update()
    {
        triggerEvent.OnUpdate();
    }


    protected virtual void OnDestroy()
    {
        ManagerObject.instance.actionManager.GetItemEvent -= GetItem;
        ManagerObject.instance.actionManager.FindInvenEvent -= FindInven;
        triggerEvent.OnDestroy();

    }



    private void GetItem(GameObject item)
    {
        //������ ȹ�� �� �ڵ����� ����Ǵ� �̺�Ʈ�� TriggerEventManager���� ��������Ʈ�� ó�� �ϵ��� �����Ͽ���.
        inven[invencount] = item;
        InvenNameSubmit();
        invencount += 1;

        GameObject itemininventoryprefab = new GameObject();
        itemininventoryprefab.AddComponent<Image>();
        InCodePrefab = GameObject.Instantiate(itemininventoryprefab); //�κ��丮�� ��ư(������)����
        InCodePrefab.transform.SetParent(ManagerObject.instance.actionManager.OnGetContentFolder().transform); //������ ��ȯ �� content �Ʒ��� ����
        InCodePrefab.GetComponent<Image>().sprite = item.GetComponent<Image>().sprite;
    }
    public void InvenNameSubmit()
    {
        invencontentname[invencount] = inven[invencount].name;
    }
    public bool FindInven(string name)
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